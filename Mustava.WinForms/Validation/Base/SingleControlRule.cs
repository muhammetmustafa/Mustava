using Mustava.WinForms.Validation.Utils;

namespace Mustava.WinForms.Validation.Base
{
    public abstract class SingleControlRule : Rule
    {
        public ControlInfo ControlInfo { get; set; }

        public override bool Validate()
        {
            if (ControlInfo == null)
                return false;

            if (!ControlInfo.IsMyPropertyValuesValid())
                return false;

            return CustomValidation();
        }
    }
}