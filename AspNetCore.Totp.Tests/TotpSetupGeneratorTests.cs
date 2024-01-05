using Xunit;

namespace AspNetCore.Totp.Tests
{
    public class TotpSetupGeneratorTests
    {
        private readonly TotpSetupGenerator totpSetupGenerator;
        public TotpSetupGeneratorTests()
        {
            this.totpSetupGenerator = new TotpSetupGenerator();
        }

        [Fact]
        public void GenerateSetupCode_shouldNotBeNull_manuelTest_workWithGoogleAuthenticator()
        {
            var totpSetup = this.totpSetupGenerator.Generate("Super Totp Tester", "Damir Kusar", "7FF3F52B-2BE1-41DF-80DE-04D32171F8A3");
            Assert.NotNull(totpSetup);
            Assert.Equal("G5DEMM2GGUZEELJSIJCTCLJUGFCEMLJYGBCEKLJQGRCDGMRRG4YUMOCBGM", totpSetup.ManualSetupKey);
            Assert.StartsWith("data:image/png;base64,", totpSetup.QrCodeImage);
        }
    }
}
