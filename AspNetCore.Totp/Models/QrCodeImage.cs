using System;
using AspNetCore.Totp.Interface;

namespace AspNetCore.Totp.Models
{
    public class QrCodeImage : IQrCodeImage
    {
        public QrCodeImage(byte[] bytes)
        {
            Bytes = bytes;
        }

        public string DataUri => @"data:image/png;base64," + Convert.ToBase64String(Bytes);
        public byte[] Bytes { get; }
    }
}