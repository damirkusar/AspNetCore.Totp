using Xunit;

namespace AspNetCore.Totp.Tests
{
    public class TotpValidatorTests
    {
        private readonly TotpValidator totpValidator;
        public TotpValidatorTests()
        {
            this.totpValidator = new TotpValidator();
        }

        [Fact]
        public void Validate_TotpGeneratedByGoogleAuthenticatorIsValid()
        {
            var validated = this.totpValidator.Validate("7FF3F52B-2BE1-41DF-80DE-04D32171F8A3", 542854);
            Assert.True(validated);
        }
    }
}
