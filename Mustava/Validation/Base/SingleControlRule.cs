using Mustava.Validation.Utils;

namespace Mustava.Validation.Base
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