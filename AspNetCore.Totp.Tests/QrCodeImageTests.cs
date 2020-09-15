using System;
using System.Text;
using AspNetCore.Totp.Models;
using Xunit;

namespace AspNetCore.Totp.Tests
{
    public class QrCodeImageTests
    {
        [Fact]
        public void No_Byte_Allocation_Side_Effects()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a byte test");
            var setup = new TotpSetup("IRRELEVANT", bytes);
            Assert.Equal(@"data:image/png;base64,VGhpcyBpcyBhIGJ5dGUgdGVzdA==", setup.QrCodeImage);
            Assert.Equal(bytes, setup.QrCodeImageBytes);
        }
    }
}