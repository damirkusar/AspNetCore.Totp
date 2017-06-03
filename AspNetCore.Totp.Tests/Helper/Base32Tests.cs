using AspNetCore.Totp.Helper;
using Xunit;

namespace AspNetCore.Totp.Tests.Helper
{
    public class Base32Tests
    {
        public Base32Tests()
        {
        }

        [Fact]
        public void Encode_ShouldCreateCorrectEncodedString()
        {
            var encoded = Base32.Encode("7FF3F52B-2BE1-41DF-80DE-04D32171F8A3");
            Assert.Equal("G5DEMM2GGUZEELJSIJCTCLJUGFCEMLJYGBCEKLJQGRCDGMRRG4YUMOCBGM", encoded);
        }

        [Fact]
        public void Decode_ShouldCreateCorrectDecodedString()
        {
            var decoded = Base32.Decode("G5DEMM2GGUZEELJSIJCTCLJUGFCEMLJYGBCEKLJQGRCDGMRRG4YUMOCBGM");
            Assert.Equal("7FF3F52B-2BE1-41DF-80DE-04D32171F8A3", decoded);
        }

        [Fact]
        public void Encode_Decode_WithDash_Succeed()
        {
            var encoded = Base32.Encode("12345-123");
            Assert.Equal("GEZDGNBVFUYTEMY", encoded);

            var decoded = Base32.Decode("GEZDGNBVFUYTEMY");
            Assert.Equal("12345-123", decoded);
        }

        [Fact]
        public void Encode_Decode_WithDollar_Succeed()
        {
            var encoded = Base32.Encode("12345$123");
            Assert.Equal("GEZDGNBVEQYTEMY", encoded);

            var decoded = Base32.Decode("GEZDGNBVEQYTEMY");
            Assert.Equal("12345$123", decoded);
        }

        [Fact]
        public void Encode_Decode_Succeed()
        {
            var encoded = Base32.Encode("12345");
            Assert.Equal("GEZDGNBV", encoded);

            var decoded = Base32.Decode("GEZDGNBV");
            Assert.Equal("12345", decoded);
        }
    }
}
