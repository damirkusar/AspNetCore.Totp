using System;
using System.Net.Http;
using AspNetCore.Totp.Helper;
using AspNetCore.Totp.Models;

namespace AspNetCore.Totp
{
    public class TotpSetupGenerator
    {
        public TotpSetupGenerator()
        {
        }

        public TotpSetup Generate(string issuer, string accountTitle, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true)
        {
            Guard.NotNull(issuer);
            Guard.NotNull(accountTitle);
            Guard.NotNull(accountSecretKey);

            accountTitle = accountTitle.Replace(" ", "");
            var encodedSecretKey = Base32.Encode(accountSecretKey);
            var provisionUrl = UrlEncoder.Encode(string.Format("otpauth://totp/{0}?secret={1}&issuer={2}", accountTitle, encodedSecretKey, UrlEncoder.Encode(issuer)));
            var protocol = useHttps ? "https" : "http";
            var url = $"{protocol}://chart.googleapis.com/chart?cht=qr&chs={qrCodeWidth}x{qrCodeHeight}&chl={provisionUrl}";

            var setup = this.GetQrImage(url);
            setup.AccountIdentity = accountTitle;
            setup.AccountSecretKey = accountSecretKey;
            setup.ManualSetupKey = encodedSecretKey;

            return setup;
        }

        private TotpSetup GetQrImage(string url, int timeoutInSeconds = 30)
        {
            try
            {
                var client = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) };
                var res = client.GetAsync(url).Result;

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var imageAsBytes = res.Content.ReadAsByteArrayAsync().Result;
                    var imageAsString = @"data:image/png;base64," + Convert.ToBase64String(imageAsBytes);

                    return new TotpSetup() { QrCodeImage = imageAsString, QrCodeByteArray = imageAsBytes, QrCodeUrl = url };
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
