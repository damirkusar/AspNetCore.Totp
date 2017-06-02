using System;
using System.Linq;
using System.Text;

namespace AspNetCore.Totp.Helper
{
    public static class Base32Encoder
    {
        private static string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static string Encode(string accountSecretKey)
        {
            var data = Encoding.UTF8.GetBytes(accountSecretKey);
            const int inByteSize = 8;
            const int outByteSize = 5;
            var alphabet = allowedCharacters.ToCharArray();

            int i = 0, index = 0;
            var result = new StringBuilder((data.Length + 7) * inByteSize / outByteSize);

            while (i < data.Length)
            {
                var currentByte = (data[i] >= 0) ? data[i] : (data[i] + 256);

                /* Is the current digit going to span a byte boundary? */
                var digit = 0;
                if (index > (inByteSize - outByteSize))
                {
                    int nextByte;
                    if ((i + 1) < data.Length)
                        nextByte = (data[i + 1] >= 0) ? data[i + 1] : (data[i + 1] + 256);
                    else
                        nextByte = 0;

                    digit = currentByte & (0xFF >> index);
                    index = (index + outByteSize) % inByteSize;
                    digit <<= index;
                    digit |= nextByte >> (inByteSize - index);
                    i++;
                }
                else
                {
                    digit = (currentByte >> (inByteSize - (index + outByteSize))) & 0x1F;
                    index = (index + outByteSize) % inByteSize;
                    if (index == 0)
                        i++;
                }
                result.Append(alphabet[digit]);
            }

            return result.ToString();
        }
    }
}
