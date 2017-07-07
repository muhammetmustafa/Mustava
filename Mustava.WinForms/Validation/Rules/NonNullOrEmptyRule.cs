using Mustava.Extensions;
using Mustava.WinForms.Validation.Base;

namespace Mustava.WinForms.Validation.Rules
{
    public class NonNullOrEmptyRule : SingleControlRule
    {
        protected override bool CustomValidation()
        {
            var value = ControlInfo.GetValue();

            if (value.ToStringOrEmpty().Trim().IsNullOrEmpty())
                return false;

            return true;
        }
    }
}