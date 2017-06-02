using System.Text;
using AspNetCore.Totp.Helper;
using AspNetCore.Totp.Models;

namespace AspNetCore.Totp
{
    public class TotpSetupCodeGenerator
    {
        public TotpSetupCodeGenerator()
        {
        }

        public TotpSetup Generate(string issuer, string accountTitle, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true)
        {
            Guard.NotNull(issuer);
            Guard.NotNull(accountTitle);
            Guard.NotNull(accountSecretKey);

            accountTitle = accountTitle.Replace(" ", "");
            var encodedSecretKey = Base32Encoder.Encode(accountSecretKey);
            var provisionUrl = UrlEncoder.Encode(string.Format("otpauth://totp/{0}?secret={1}&issuer={2}", accountTitle, encodedSecretKey, UrlEncoder.Encode(issuer)));
            var protocol = useHttps ? "https" : "http";
            var url = $"{protocol}://chart.googleapis.com/chart?cht=qr&chs={qrCodeWidth}x{qrCodeHeight}&chl={provisionUrl}";

            var setup = new TotpSetup
            {
                AccountTitle = accountTitle,
                AccountSecretKey = accountSecretKey,
                ManualSetupKey = encodedSecretKey,
                QrCodeSetupImageUrl = url
            };

            return setup;
        }
    }
}
