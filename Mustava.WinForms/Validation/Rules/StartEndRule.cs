using System;
using Mustava.Extensions;
using Mustava.WinForms.Validation.Base;

namespace Mustava.WinForms.Validation.Rules
{
    public class StartEndRule : TwoControlRule
    {
        protected override bool CustomValidation()
        {
            var start = ControlInfo1.GetValue();
            if (!(start is DateTime))
                return false;

            var end = ControlInfo2.GetValue();
            if (!(end is DateTime))
                return false;

            var startDateTime = (DateTime) start;
            var endDateTime = (DateTime) end;

            if (startDateTime.isMinOrMax() || endDateTime.isMinOrMax())
                return false;

            if (startDateTime >= endDateTime)
                return false;

            return true;
        }
    }
}