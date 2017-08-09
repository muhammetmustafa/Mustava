using System;
using System.Globalization;
using System.Windows.Forms;

namespace Mustava.Extensions
{
    public static class ObjectExtensions
    {
        public static byte ToByte(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            byte resultValue;
            return byte.TryParse(item, out resultValue) ? resultValue : (byte)0;
        }

        public static byte? ToNullByte(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return null;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            byte resultValue;
            return byte.TryParse(item, out resultValue) ? resultValue : (byte?)null;
        }
        
        public static long ToLong(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            long resultValue;
            return long.TryParse(item, out resultValue) ? resultValue : 0;
        }

        public static double ToDouble(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var item = ToDecimalSeperatorFixedFormat(value.ToString());

            double resultValue;
            if (double.TryParse(item, out resultValue))
                return resultValue;
            return 0;
        }
        
        public static bool ToBoolen(this object value)
        {
            if (value == null)
                return false;

            bool result;

            if (bool.TryParse(value.ToString(), out result))
                return result;

            return result;
        }
        
        public static int ToInt(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            int resultValue;
            if (int.TryParse(item, out resultValue))
                return resultValue;
            return 0;
        }
        
        public static DateTime ToDateTimeX(this object value)
        {
            var result = default(DateTime);

            if (value == null)
                return result;

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
            var result = default(DateTime);

            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return result;
        }
        
        public static DateTime? ToNullDateTime(this object value)
        {
            var result = default(DateTime);
            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return default(DateTime?);
        }
        
        public static long? ToNullLong(this object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);

            long resultValue;

            if (long.TryParse(item, out resultValue))
                return resultValue;

            return null;
        }

        public static int? ToNullInt(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return null;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);

            int resultValue;
            if (int.TryParse(item, out resultValue))
                return resultValue;

            return null;
        }

        public static decimal? GetDecimalValue2(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return null;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            decimal resultValue;
            if (decimal.TryParse(item, out resultValue))
                return resultValue;
            return null;
        }

        public static double? GetDoubleValue2(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return null;

            var item = ToDecimalSeperatorFixedFormat(value.ToString());

            double resultValue;
            if (double.TryParse(item, out resultValue))
                return resultValue;
            return null;
        }
        
        public static string ToDecimalSeperatorFixedFormat(this string value)
        {
            var decimalSeperator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            value = value.Replace(decimalSeperator == "." ? "," : ".", decimalSeperator);

            return value;
        }
        
        public static string ToStringOrEmpty(this object o)
        {
            return o == null ? "" : o.ToString();
        }

        public static string ToStringOrDefault(this object o, string defaultText)
        {
            return o == null ? defaultText : o.ToString();
        }

        public static bool IsNullOrEmpty(this object s)
        {
            if (s == null)
                return true;

            return s.ToStringOrEmpty() == string.Empty;
        }

        public static BindingSource AsBindingSource(this object obj, string dataMember = "")
        {
            return new BindingSource(obj, dataMember);
        }
    }
}