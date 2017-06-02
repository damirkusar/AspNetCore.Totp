using Xunit;

namespace AspNetCore.Totp.Tests
{
    public class TotpSetupCodeValidatorTests
    {
        private readonly TotpSetupCodeValidator totpSetupCodeValidator;
        public TotpSetupCodeValidatorTests()
        {
            this.totpSetupCodeValidator = new TotpSetupCodeValidator();
        }

        [Fact]
        public void Validate_TotpGeneratedByGoogleAuthenticatorIsValid()
        {
            var validated = this.totpSetupCodeValidator.Validate("7FF3F52B-2BE1-41DF-80DE-04D32171F8A3", 442472);
            Assert.True(validated);
        }
    }
}
