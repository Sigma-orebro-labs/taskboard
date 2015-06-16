using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBoard.Web.Extensions
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string s, string toTrim)
        {
            return s.Substring(0, s.Length - toTrim.Length);
        }
    }
}
