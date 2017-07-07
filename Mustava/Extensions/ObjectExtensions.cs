using System;
using System.Globalization;

namespace Mustava.Extensions
{
    public static class ObjectExtensions
    {
        public static long ToLong(this object value)
        {
            long result = 0;
            if (value == null) return result;
            if (Int64.TryParse(value.ToString(), out result))
                return result;
            return result;
        }

        public static double ToDouble(this object value)
        {
            double result = 0;
            if (value == null) return result;
            if (Double.TryParse(value.ToString(), out result))
                return result;
            return result;
        }
        
        public static bool ToBoolen(this object value)
        {
            bool result = false;
            if (value == null) return result;
            if (Boolean.TryParse(value.ToString(), out result))
                return result;
            return result;
        }
        
        public static int ToInt(this object value)
        {
            int result = 0;

            if (value == null)
                return result;

            if (value is float)
                return Convert.ToInt32(value);

            if (Int32.TryParse(value.ToString(), out result))
                return result;
            return result;
        }
        
        public static DateTime ToDateTimeX(this object value)
        {
            DateTime result = default(DateTime);
            if (value == null) return result;
            if (DateTime.TryParse(value.ToString(), out result))
                return result;
            return result;
        }

        public static DateTime ToDateTimeX(this object value, string format)
        {
            var result = default(DateTime);

            if (value == null) 
                return result;

            if (DateTime.TryParseExact(value.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;

            return result;
        }

        public static DateTime ToDateTimeX(this object value, DateTime defaultValue)
        {
            var result = defaultValue;

            if (value == null) 
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            if (result.Equals(default(DateTime)))
                result = defaultValue;return result;
        }

        public static DateTime ToDateTime(this DateTime? value, DateTime defaultValue)
        {
            var result = defaultValue;

            if (value == null)
                return result;

            return (DateTime) value;
        }
        
        public static long? ToNullLong(this object value)
        {
            long result = 0;
            if (value == null) return null;
            if (Int64.TryParse(value.ToString(), out result))
                return result;
            return null;
        }
        public static int? ToNullInt(this object value)
        {
            int result = 0;
            if (value == null) return null;
            if (Int32.TryParse(value.ToString(), out result))
                return result;
            return (int?)null;
        }
        public static decimal? GetDecimalValue2(this object value)
        {
            string seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            if (value == null || value == DBNull.Value) return null;
            string item = value.ToString();
            item.Replace(".", seperator);
            item.Replace(",", seperator);
            decimal resultValue;
            if (Decimal.TryParse(item, out resultValue))
                return resultValue;
            return null;
        }
        public static int? GetIntergerValue2(this object value)
        {
            string seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            if (value == null || value == DBNull.Value) return null;
            string item = value.ToString();
            item.Replace(".", seperator);
            item.Replace(",", seperator);
            int resultValue;
            if (Int32.TryParse(item, out resultValue))
                return resultValue;
            return null;
        }
        public static double? GetDoubleValue2(this object value)
        {

            if (value == null || value == DBNull.Value) return null;
            string item = value.ToString().ToDecimalSeperatorFixedFormat();
            double resultValue;
            if (Double.TryParse(item, out resultValue))
                return resultValue;
            return null;
        }

        public static string ToStringOrEmpty(this object o)
        {
            return o == null ? "" : o.ToString();
        }

        public static string ToStringOrDefault(this object o, string defaultText)
        {
            return o == null ? defaultText : o.ToString();
        }

        public static string toDateTimeString(this object s)
        {
            if (s == null) return "";
            return DateTime.Parse(s.ToString()).ToString("dd/MM/yyyy hh:mm:ss").Replace('.', '/');
        }

        public static bool IsNullOrEmpty(this object s)
        {
            if (s == null)
                return true;

            return s.ToStringOrEmpty() == string.Empty;
        }
    }
}