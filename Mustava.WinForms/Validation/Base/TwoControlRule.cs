using Mustava.WinForms.Validation.Utils;

namespace Mustava.WinForms.Validation.Base
{
    public abstract class TwoControlRule : Rule
    {
        public ControlInfo ControlInfo1 { get; set; }

        public ControlInfo ControlInfo2 { get; set; }

        public override bool Validate()
        {
            if (ControlInfo1 == null)
                return false;

            if (ControlInfo2 == null)
                return false;

            if (!ControlInfo1.IsMyPropertyValuesValid())
                return false;

            if (!ControlInfo2.IsMyPropertyValuesValid())
                return false;

            return CustomValidation();
        }
    }
}