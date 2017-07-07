using System.Collections.Generic;
using Mustava.WinForms.Validation.Base;
using Mustava.WinForms.Validation.Utils;

namespace Mustava.WinForms.Validation.ValidationItems
{
    public class ValidationItem : BaseValidationItem
    {
        public List<ControlInfo> ControlInfos { get; private set; }

        public ValidationItem()
        {
            ControlInfos = new List<ControlInfo>();
            Rules = new List<Rule>();
        }

        public override void DoValidate()
        {
            if (MatchRulesWithControlInfos() == false)
                Validity = false;

            Validity = true;

            foreach (var rule in Rules)
            {
                Validity &= rule.Validate();
            }

            AfterValidation();
        }

        public override void AfterValidation()
        {
            InvalidControls.Clear();

            foreach (var controlInfo in ControlInfos)
            {
                setStyle(controlInfo);

                if (Validity == false)
                {
                    InvalidControls.Add(controlInfo.Control);
                }
            }
        }

        private bool MatchRulesWithControlInfos()
        {
            if (ControlInfos.Count <= 0)
                return false;

            if (Rules.Count <= 0)
                return false;

            foreach (var rule in Rules)
            {
                var singleControlRule = rule as SingleControlRule;
                if (singleControlRule != null)
                {
                    singleControlRule.ControlInfo = ControlInfos[0];
                }

                if (ControlInfos.Count <= 1)
                    continue;
                var twoControlRule = rule as TwoControlRule;
                if (twoControlRule != null)
                {
                    twoControlRule.ControlInfo1 = ControlInfos[0];
                    twoControlRule.ControlInfo2 = ControlInfos[1];
                }
            }

            return true;
        }

        public bool AddControlInfo(ControlInfo controlInfo)
        {
            if (controlInfo == null)
                return false;

            ControlInfos.Add(controlInfo);
            return true;
        }
    }
}