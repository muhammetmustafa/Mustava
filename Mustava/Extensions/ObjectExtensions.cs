using System;
using System.Globalization;
using System.Windows.Forms;

namespace Mustava.Extensions
{
    public static class ObjectExtensions
    {
        public static byte ExToByte(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            byte resultValue;
            return byte.TryParse(item, out resultValue) ? resultValue : (byte)0;
        }

        public static byte? ExToNullByte(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return null;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            byte resultValue;
            return byte.TryParse(item, out resultValue) ? resultValue : (byte?)null;
        }
        
        public static long ExToLong(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var seperator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            var item = value.ToString().Replace(".", seperator).Replace(",", seperator);
            long resultValue;
            return long.TryParse(item, out resultValue) ? resultValue : 0;
        }

        public static double ExToDouble(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return 0;

            var item = ExToDecimalSeperatorFixedFormat(value.ToString());

            double resultValue;
            if (double.TryParse(item, out resultValue))
                return resultValue;
            return 0;
        }
        
        public static bool ExToBoolen(this object value)
        {
            if (value == null)
                return false;

            bool result;

            if (bool.TryParse(value.ToString(), out result))
                return result;

            return result;
        }
        
        public static int ExToInt(this object value)
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
        
        public static DateTime ExToDateTimeX(this object value)
        {
            var result = default(DateTime);

            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return result;
        }

        public static DateTime ExToDateTimeX(this object value, string format)
        {
            var result = default(DateTime);

            if (value == null) 
                return result;

            if (DateTime.TryParseExact(value.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;

            return result;
        }

        public static DateTime ExToDateTimeX(this object value, DateTime defaultValue)
        {
            var result = defaultValue;

            if (value == null) 
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            if (result.Equals(default(DateTime)))
                result = defaultValue;return result;
        }

        public static DateTime ExToDateTime(this DateTime? value, DateTime defaultValue)
        {
            var result = default(DateTime);

            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return result;
        }
        
        public static DateTime? ExToNullDateTime(this object value)
        {
            var result = default(DateTime);
            if (value == null)
                return result;

            if (DateTime.TryParse(value.ToString(), out result))
                return result;

            return default(DateTime?);
        }
        
        public static long? ExToNullLong(this object value)
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

        public static int? ExToNullInt(this object value)
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

        public static decimal? ExGetDecimalValue2(this object value)
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

        public static double? ExGetDoubleValue2(this object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
                return null;

            var item = ExToDecimalSeperatorFixedFormat(value.ToString());

            double resultValue;
            if (double.TryParse(item, out resultValue))
                return resultValue;
            return null;
        }
        
        public static string ExToDecimalSeperatorFixedFormat(this string value)
        {
            var decimalSeperator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            value = value.Replace(decimalSeperator == "." ? "," : ".", decimalSeperator);

            return value;
        }
        
        public static string ExToStringOrEmpty(this object o)
        {
            return o == null ? "" : o.ToString();
        }

        public static string ExToStringOrDefault(this object o, string defaultText)
        {
            return o == null ? defaultText : o.ToString();
        }

        public static bool ExIsNullOrEmpty(this object s)
        {
            if (s == null)
                return true;

            return s.ExToStringOrEmpty() == string.Empty;
        }

        public static BindingSource AsBindingSource(this object obj, string dataMember = "")
        {
            return new BindingSource(obj, dataMember);
        }
    }
}