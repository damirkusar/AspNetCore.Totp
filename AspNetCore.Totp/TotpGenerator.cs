using System;
using System.Collections.Generic;
using AspNetCore.Totp.Helper;

namespace AspNetCore.Totp
{
    public class TotpGenerator
    {
        private readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public TotpGenerator()
        {
        }

        internal IEnumerable<int> GetValidTotps(string accountSecretKey, TimeSpan timeTolerance)
        {
            var codes = new List<int>();
            var iterationCounter = this.GetCurrentCounter();
            var iterationOffset = 0;

            if (timeTolerance.TotalSeconds > 30)
            {
                iterationOffset = Convert.ToInt32(timeTolerance.TotalSeconds / 30.00);
            }

            var iterationStart = iterationCounter - iterationOffset;
            var iterationEnd = iterationCounter + iterationOffset;

            for (var counter = iterationStart; counter <= iterationEnd; counter++)
            {
                codes.Add(this.Generate(accountSecretKey, counter));
            }

            return codes.ToArray();
        }

        internal int Generate(string accountSecretKey, long counter, int digits = 6)
        {
            return TotpHasher.Hash(accountSecretKey, counter, digits);
        }

        public int Generate(string accountSecretKey, int digits = 6)
        {
            return TotpHasher.Hash(accountSecretKey, this.GetCurrentCounter(), digits);
        }

        private long GetCurrentCounter()
        {
            return (long)(DateTime.UtcNow - this.unixEpoch).TotalSeconds / 30;
        }

    }
}
