using Mustava.Extensions;
using Mustava.WinForms.Validation.Base;

namespace Mustava.WinForms.Validation.Rules
{
    public class NonNullOrEmptyRule : SingleControlRule
    {
        protected override bool CustomValidation()
        {
            var value = ControlInfo.GetValue();

            if (value.ExToStringOrEmpty().Trim().ExIsNullOrEmpty())
                return false;

            return true;
        }
    }
}