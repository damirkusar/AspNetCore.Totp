using AspNetCore.Totp.Interface.Models;

namespace AspNetCore.Totp.Interface
{
    public interface ITotpSetupGenerator
    {
        /// <summary>
        /// Generates an object you will need so that the user can setup his Google Authenticator to be used with your app.
        /// </summary>
        /// <param name="issuer">Your app name or company for example.</param>
        /// <param name="accountIdentity">Name, Email or Id of the user, without spaces, this will be shown in google authenticator.</param>
        /// <param name="accountSecretKey">A secret key which will be used to generate one time passwords. This key is the same needed for validating a passed TOTP.</param>
        /// <param name="qrCodeWidth">Height of the QR code. Default is 300px.</param>
        /// <param name="qrCodeHeight">Width of the QR code. Default is 300px.</param>
        /// <param name="useHttps">Use Https on google api or not.</param>
        /// <returns>TotpSetup with ManualSetupKey and QrCode.</returns>
        TotpSetup Generate(string issuer, string accountIdentity, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true);
    }
}
