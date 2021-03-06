﻿using System;
using System.Globalization;
using System.Linq;

namespace Mustava.Extensions
{
    public static class StringExtensions
    {
        public static bool ExIsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool ExIsNumeric(this string s)
        {
            return s.All(char.IsDigit);
        }

        public static string ExToDecimalSeperatorFixedFormat(this string value)
        {
            var decimalSeperator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return value.Replace(decimalSeperator == "." ? "," : ".", decimalSeperator);
        }

        public static int ExToMinutes(this string hour)
        {
            if (hour.ExIsNullOrEmpty())
                return 0;

            var saatler = hour.Split(':');
            if (saatler.Length < 2)
                return 0;

            return saatler[0].ExToInt() * 60 + saatler[1].ExToInt();
        }

        public static string ReplaceSafe(this string str, string oldValue, string newValue)
        {
            if (str == null)
                return string.Empty;

            if (str.Contains(oldValue))
                return str.Replace(oldValue, newValue);
            else
                return str;
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            var pos = text.IndexOf(search, StringComparison.OrdinalIgnoreCase);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static bool EqualsNoMatter(this string str, string other)
        {
            return str.Equals(other, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EqualsNoMatterOred(this string str, params string[] others)
        {
            var result = false;

            foreach (var other in others)
            {
                result |= str.EqualsNoMatter(other);
            }

            return result;
        }
    }
}