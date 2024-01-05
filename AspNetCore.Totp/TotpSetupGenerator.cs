using System;
using System.IO;
using AspNetCore.Totp.Helper;
using AspNetCore.Totp.Interface;
using AspNetCore.Totp.Models;
using QRCoder;
using SkiaSharp;

namespace AspNetCore.Totp
{
    public class TotpSetupGenerator : ITotpSetupGenerator
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
        public ITotpSetup Generate(string issuer, string accountIdentity, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true)
        {
            Guard.NotNull(issuer);
            Guard.NotNull(accountIdentity);
            Guard.NotNull(accountSecretKey);
            
            var encodedSecretKey = Base32.Encode(accountSecretKey);
            
            var generator = new PayloadGenerator.OneTimePassword()
            {
                Secret = encodedSecretKey,
                Issuer = issuer,
                Label = accountIdentity.Replace(" ", "")
            };
            
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(generator.ToString(), QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            
            return new TotpSetup(encodedSecretKey, ScaleImage(qrCode.GetGraphic(20), qrCodeWidth, qrCodeHeight));
        }

        private static byte[] ScaleImage(byte[] imageBytes, int maxWidth, int maxHeight)
        {
            var image = SKBitmap.Decode(imageBytes);

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var info = new SKImageInfo(newWidth, newHeight);
            image = image.Resize(info, SKFilterQuality.High);

            using var ms = new MemoryStream();
            image.Encode(ms, SKEncodedImageFormat.Png, 100);
            return ms.ToArray();
        }
    }
}
