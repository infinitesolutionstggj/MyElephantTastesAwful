using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace META.Engine
{
    public static class StringExt
    {
        public static string SplitCamelCase(this string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                System.Text.RegularExpressions.Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
    }
}
