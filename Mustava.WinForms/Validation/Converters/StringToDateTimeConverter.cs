using Mustava.Extensions;

namespace Mustava.WinForms.Validation.Converters
{
    public class StringToDateTimeConverter : Converter
    {
        protected override object doConvert(object obj)
        {
            var s = obj as string;
            if (s == null)
                return null;

            return s.ExToDateTimeX();
        }
    }
}