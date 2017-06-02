using Xunit;

namespace AspNetCore.Totp.Tests
{
    public class TotpSetupCodeGeneratorTests
    {
        private readonly TotpSetupCodeGenerator totpSetupCodeGenerator;
        public TotpSetupCodeGeneratorTests()
        {
            this.totpSetupCodeGenerator = new TotpSetupCodeGenerator();
        }

        [Fact]
        public void GenerateSetupCode_shouldNotBeNull_manuelTest_workWithGoogleAuthenticator()
        {
            var totpSetup = this.totpSetupCodeGenerator.Generate("Super Totp Tester 2", "Damir Kusar", "7FF3F52B-2BE1-41DF-80DE-04D32171F8A3");
            Assert.NotNull(totpSetup);
        }
    }
}
