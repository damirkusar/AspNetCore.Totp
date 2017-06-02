using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Totp.Helper
{
    public static class Guard
    {
        public static void NotNull(object testee)
        {
            if (testee == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
