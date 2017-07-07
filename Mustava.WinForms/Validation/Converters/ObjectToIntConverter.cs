using Mustava.Extensions;

namespace Mustava.WinForms.Validation.Converters
{
    public class ObjectToIntConverter : Converter
    {
        protected override object doConvert(object obj)
        {
            return obj.ToInt();
        }
    }
}