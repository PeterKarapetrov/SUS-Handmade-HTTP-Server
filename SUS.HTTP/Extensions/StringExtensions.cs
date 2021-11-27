using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string text) => char.ToUpper(text[0]) + text.Substring(1).ToLower();
    }
}
