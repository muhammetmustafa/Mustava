﻿using System;

namespace Mustava.WinForms.Validation.Converters
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