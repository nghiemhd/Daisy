using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Common.Extensions
{
    public static class StringExtentions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return string.IsNullOrEmpty(str.Trim());
            }
        }
    }
}
