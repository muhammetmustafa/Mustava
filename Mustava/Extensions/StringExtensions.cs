using System;
using System.Globalization;
using System.Linq;

namespace Mustava.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNumeric(this string s)
        {
            return s.All(Char.IsDigit);
        }

        public static string ToDecimalSeperatorFixedFormat(this string value)
        {
            var decimalSeperator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return value.Replace(decimalSeperator == "." ? "," : ".", decimalSeperator);
        }

        public static int ToMinutes(this string hour)
        {
            if (hour.IsNullOrEmpty())
                return 0;

            var saatler = hour.Split(':');
            if (saatler.Length < 2)
                return 0;

            return saatler[0].ToInt() * 60 + saatler[1].ToInt();
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

        public static Base64ImageValue GetBase64ImageData(this string content)
        {
            var result1 = content.Split(';');
            var extensions = result1[0].Split('/')[1];
            var contentResult = result1[1].Replace("base64,", "");

            return new Base64ImageValue()
            {
                Value = contentResult,
                Extensions = extensions
            };
        }
    }
}