using System;

namespace AspNetCore.Totp.Models
{
    public class TotpSetup
    {
        public TotpSetup() { }

        /// <summary>
        /// Name, Email or Id of the user, this will be shown in google authenticator.
        /// </summary>
        //public string AccountIdentity { get; internal set; }

        /// <summary>
        /// A secret key which will be used to generate one time passwords. This key is the same needed for validating a passed TOTP.
        /// </summary>
        //public string AccountSecretKey { get; internal set; }

        /// <summary>
        /// If the QR code can not be used, this code is needed to setup Google Authenticator. 
        /// </summary>
        public string ManualSetupKey { get; internal set; }

        /// <summary>
        /// URL of the QR code on Google APIs.
        /// </summary>
        //public string QrCodeUrl { get; internal set; }

        /// <summary>
        /// Byte Array of the image. Needs data:image/png;base64, + byte array on UI for the image tag. 
        /// </summary>
        //public byte[] QrCodeByteArray { get; internal set; }

        /// <summary>
        /// Image string ready to be used in image tag without modification. 
        /// </summary>
        public string QrCodeImage { get; internal set; }
    }
}