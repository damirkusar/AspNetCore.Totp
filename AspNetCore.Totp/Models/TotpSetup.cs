using System;

namespace AspNetCore.Totp.Models
{
    public class TotpSetup
    {
        public TotpSetup() { }

        public string AccountIdentity { get; internal set; }
        public string AccountSecretKey { get; internal set; }
        public string ManualSetupKey { get; internal set; }
        public string QrCodeUrl { get; internal set; }
        // needs data:image/png;base64, + byte array on UI
        public byte[] QrCodeByteArray { get; internal set; }
        public string QrCodeImage { get; internal set; }
    }
}
