using System;

namespace Mustava.Validation.Converters
{
    public class DateTimeToStringConverter : Converter
    {
        protected override object doConvert(object obj)
        {
            if (obj is DateTime)
            {
                return ((DateTime) obj).ToString();
            }

            return null;
        }
    }
}