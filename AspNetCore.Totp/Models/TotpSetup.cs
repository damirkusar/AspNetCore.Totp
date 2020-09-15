using AspNetCore.Totp.Interface;

namespace AspNetCore.Totp.Models
{
    public class TotpSetup : ITotpSetup
    {
        private readonly IQrCodeImage _qrCodeImage;

        public TotpSetup(string manualSetupKey, byte[] imageBytes)
        {
            ManualSetupKey = manualSetupKey;
            _qrCodeImage = new QrCodeImage(imageBytes);
        }

        /// <summary>
        ///     If the QR code can not be used, this code is needed to setup Google Authenticator.
        /// </summary>
        public string ManualSetupKey { get; }

        /// <summary>
        ///     Provides a Uri formatted byte string ready for data attributes
        /// </summary>
        public string QrCodeImage => _qrCodeImage.DataUri;

        /// <summary>
        ///     The byte array for the QrCode
        /// </summary>
        public byte[] QrCodeImageBytes => _qrCodeImage.Bytes;
    }
}