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
            Assert.Equal("data:image/png;base64,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", totpSetup.QrCodeImage);
        }
    }
}
