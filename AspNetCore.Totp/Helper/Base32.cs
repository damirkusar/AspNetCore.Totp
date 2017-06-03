using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Totp.Helper
{
    internal static class Base32
    {
        private static string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        internal static string Encode(string accountSecretKey)
        {
            var data = Encoding.UTF8.GetBytes(accountSecretKey);
            var output = "";
            for (var bitIndex = 0; bitIndex < data.Length * 8; bitIndex += 5)
            {
                var dualbyte = data[bitIndex / 8] << 8;
                if (bitIndex / 8 + 1 < data.Length)
                    dualbyte |= data[bitIndex / 8 + 1];
                dualbyte = 0x1f & (dualbyte >> (16 - bitIndex % 8 - 5));
                output += allowedCharacters[dualbyte];
            }

            return output;
        }

        internal static string Decode(string base32)
        {
            var output = new List<byte>();
            var bytes = base32.ToCharArray();
            for (var bitIndex = 0; bitIndex < base32.Length * 5; bitIndex += 8)
            {
                var dualbyte = allowedCharacters.IndexOf(bytes[bitIndex / 5]) << 10;
                if (bitIndex / 5 + 1 < bytes.Length)
                    dualbyte |= allowedCharacters.IndexOf(bytes[bitIndex / 5 + 1]) << 5;
                if (bitIndex / 5 + 2 < bytes.Length)
                    dualbyte |= allowedCharacters.IndexOf(bytes[bitIndex / 5 + 2]);

                dualbyte = 0xff & (dualbyte >> (15 - bitIndex % 5 - 8));
                output.Add((byte)(dualbyte));
            }

            var key = Encoding.UTF8.GetString(output.ToArray());
            if (key.EndsWith("\0"))
            {
                var index = key.IndexOf("\0", StringComparison.Ordinal);
                //key = key.Replace("\0", "");
                key = key.Remove(index, 1);
            }
            return key;
        }

        //public static string Encode(string accountSecretKey)
        //{
        //    var data = Encoding.UTF8.GetBytes(accountSecretKey);
        //    const int inByteSize = 8;
        //    const int outByteSize = 5;
        //    var alphabet = allowedCharacters.ToCharArray();

        //    int i = 0, index = 0;
        //    var result = new StringBuilder((data.Length + 7) * inByteSize / outByteSize);

        //    while (i < data.Length)
        //    {
        //        var currentByte = (data[i] >= 0) ? data[i] : (data[i] + 256);

        //        /* Is the current digit going to span a byte boundary? */
        //        var digit = 0;
        //        if (index > (inByteSize - outByteSize))
        //        {
        //            int nextByte;
        //            if ((i + 1) < data.Length)
        //                nextByte = (data[i + 1] >= 0) ? data[i + 1] : (data[i + 1] + 256);
        //            else
        //                nextByte = 0;

        //            digit = currentByte & (0xFF >> index);
        //            index = (index + outByteSize) % inByteSize;
        //            digit <<= index;
        //            digit |= nextByte >> (inByteSize - index);
        //            i++;
        //        }
        //        else
        //        {
        //            digit = (currentByte >> (inByteSize - (index + outByteSize))) & 0x1F;
        //            index = (index + outByteSize) % inByteSize;
        //            if (index == 0)
        //                i++;
        //        }
        //        result.Append(alphabet[digit]);
        //    }

        //    return result.ToString();
        //}
    }
}
