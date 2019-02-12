using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringEx
    {
        public static int ToInt32(this string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return 0;
            var ret = 0;
            if (!Int32.TryParse(strValue, out ret)) return 0;
            return ret;
        }
    }
}
