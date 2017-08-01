using System.Windows.Forms;
using Mustava.Helper;
using Mustava.Helpers;
using Mustava.WinForms.Validation.Converters;

namespace Mustava.WinForms.Validation.Utils
{
    public class ControlInfo
    {
        public Control Control { get; set; }

        public string PropertyName { get; set; }

        public Converter Converter { get; set; }

        public ControlInfo(Control control, string propertyName)
        {
            Control = control;
            PropertyName = propertyName;
        }

        public object GetValue()
        {
            var obj = Control.GetValueOfProperty(PropertyName, true);
            if (Converter != null)
            {
                obj = Converter.Convert(obj);
            }

            return obj;
        }

        public bool IsMyPropertyValuesValid()
        {
            if (Control == null || PropertyName == null)
                return false;

            if (!Control.HasProperty(PropertyName))
                return false;

            return true;
        }
    }
}