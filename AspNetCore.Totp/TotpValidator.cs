using System;
using System.Linq;

namespace AspNetCore.Totp
{
    public class TotpValidator
    {
        private readonly TotpGenerator totpGenerator;
        private const int DefaultClockDriftToleranceInSeconds = 300;

        public TotpValidator()
        {
            this.totpGenerator = new TotpGenerator();
        }

        public bool Validate(string accountSecretKey, int clientTotp, int timeToleranceInSeconds = DefaultClockDriftToleranceInSeconds)
        {
            var codes = this.totpGenerator.GetValidTotps(accountSecretKey, TimeSpan.FromSeconds(timeToleranceInSeconds));
            return codes.Any(c => c == clientTotp);
        }
    }
}
