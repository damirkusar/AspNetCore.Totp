using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Totp.Helper;
using AspNetCore.Totp.Models;

namespace AspNetCore.Totp
{
    public class TotpSetupCodeValidator
    {
        private readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private const int DefaultClockDriftToleranceInSeconds = 300;

        public TotpSetupCodeValidator()
        {
        }

        public bool Validate(string accountSecretKey, int clientTotp, int timeToleranceInSeconds = DefaultClockDriftToleranceInSeconds)
        {
            var codes = this.GetCurrentPiNs(accountSecretKey, TimeSpan.FromSeconds(timeToleranceInSeconds));
            return codes.Any(c => c == clientTotp);
        }

        private IEnumerable<int> GetCurrentPiNs(string accountSecretKey, TimeSpan timeTolerance)
        {
            var codes = new List<int>();
            var iterationCounter = this.GetCurrentCounter(DateTime.UtcNow, this.unixEpoch);
            var iterationOffset = 0;

            if (timeTolerance.TotalSeconds > 30)
            {
                iterationOffset = Convert.ToInt32(timeTolerance.TotalSeconds / 30.00);
            }

            var iterationStart = iterationCounter - iterationOffset;
            var iterationEnd = iterationCounter + iterationOffset;

            for (var counter = iterationStart; counter <= iterationEnd; counter++)
            {
                codes.Add(this.GenerateTotp(accountSecretKey, counter));
            }

            return codes.ToArray();
        }
        
        private long GetCurrentCounter(DateTime now, DateTime epoch, int timeStep = DefaultClockDriftToleranceInSeconds)
        {
            return (long)(now - epoch).TotalSeconds / timeStep;
        }

        private int GenerateTotp(string accountSecretKey, long counter, int digits = 6)
        {
            return TotpHasher.Hash(accountSecretKey, counter, digits);
        }
    }
}
