namespace AspNetCore.Totp.Models
{
    public class TotpSetup
    {
        public TotpSetup() { }

        public string AccountTitle { get; internal set; }
        public string AccountSecretKey { get; internal set; }
        public string ManualSetupKey { get; internal set; }
        public string QrCodeSetupImageUrl { get; internal set; }
    }
}
