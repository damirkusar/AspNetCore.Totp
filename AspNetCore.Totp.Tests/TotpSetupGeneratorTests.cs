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
            Assert.Equal("DamirKusar", totpSetup.AccountIdentity);
            Assert.Equal("7FF3F52B-2BE1-41DF-80DE-04D32171F8A3", totpSetup.AccountSecretKey);
            Assert.Equal("G5DEMM2GGUZEELJSIJCTCLJUGFCEMLJYGBCEKLJQGRCDGMRRG4YUMOCBGM", totpSetup.ManualSetupKey);
            Assert.Equal("https://chart.googleapis.com/chart?cht=qr&chs=300x300&chl=otpauth%3A%2F%2Ftotp%2FDamirKusar%3Fsecret%3DG5DEMM2GGUZEELJSIJCTCLJUGFCEMLJYGBCEKLJQGRCDGMRRG4YUMOCBGM%26issuer%3DSuper%2520Totp%2520Tester", totpSetup.QrCodeUrl);
        }
    }
}
