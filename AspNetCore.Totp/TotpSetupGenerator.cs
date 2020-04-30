using System;
using System.Net.Http;
using System.Web;
using AspNetCore.Totp.Helper;
using AspNetCore.Totp.Interface;
using AspNetCore.Totp.Interface.Models;

namespace AspNetCore.Totp
{
    public class TotpSetupGenerator : ITotpSetupGenerator
    {
        /// <summary>
        /// Generates an object you will need so that the user can setup his Google Authenticator to be used with your app.
        /// </summary>
        /// <param name="issuer">Your app name or company for example.</param>
        /// <param name="accountIdentity">Name, Email or Id of the user, this will be shown in google authenticator.</param>
        /// <param name="accountSecretKey">A secret key which will be used to generate one time passwords. This key is the same needed for validating a passed TOTP.</param>
        /// <param name="qrCodeWidth">Height of the QR code. Default is 300px.</param>
        /// <param name="qrCodeHeight">Width of the QR code. Default is 300px.</param>
        /// <param name="useHttps">Use Https on google api or not.</param>
        /// <returns>TotpSetup with ManualSetupKey and QrCode.</returns>
        public TotpSetup Generate(string issuer, string accountIdentity, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true)
        {
            Guard.NotNull(issuer);
            Guard.NotNull(accountIdentity);
            Guard.NotNull(accountSecretKey);

            accountIdentity = accountIdentity.Trim();
            var encodedSecretKey = Base32.Encode(accountSecretKey);
            var provisionUrl = HttpUtility.UrlEncode($"otpauth://totp/{accountIdentity}?secret={encodedSecretKey}&issuer={issuer}");
            var protocol = useHttps ? "https" : "http";
            var url = $"{protocol}://chart.googleapis.com/chart?cht=qr&chs={qrCodeWidth}x{qrCodeHeight}&chl={provisionUrl}";

            var totpSetup = new TotpSetup
            {
                QrCodeImage = this.GetQrImage(url),
                ManualSetupKey = encodedSecretKey
            };

            return totpSetup;
        }

        private string GetQrImage(string url, int timeoutInSeconds = 30)
        {
            try
            {
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) };
                var res = client.GetAsync(url).Result;

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var imageAsBytes = res.Content.ReadAsByteArrayAsync().Result;
                    var imageAsString = @"data:image/png;base64," + Convert.ToBase64String(imageAsBytes);

                    return imageAsString;
                }
                else
                {
                    throw new Exception("Unexpected result from the Google QR web site.");
                }
            }
            catch (Exception exception)
            {
                throw new HttpRequestException("Unexpected result from the Google QR web site.", exception);
            }
        }
    }
}
