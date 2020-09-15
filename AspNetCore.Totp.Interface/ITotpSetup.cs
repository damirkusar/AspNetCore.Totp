namespace AspNetCore.Totp.Interface
{
    public interface ITotpSetup
    {
        /// <summary>
        ///     Provides a Uri formatted byte string ready for data attributes
        /// </summary>
        string QrCodeImage { get; }

        /// <summary>
        ///     The byte array for the QrCode
        /// </summary>
        byte[] QrCodeImageBytes { get; }

        /// <summary>
        ///     If the QR code can not be used, this code is needed to setup Google Authenticator.
        /// </summary>
        string ManualSetupKey { get; }
    }
}