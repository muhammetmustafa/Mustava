using System.Collections.Generic;
using Mustava.Validation.Base;
using Mustava.Validation.Utils;

namespace Mustava.Validation.ValidationItems
{
    public abstract class BaseValidationItem
    {
        public List<Control> InvalidControls = new List<Control>(); 

        public bool Validity { get; set; }

        public List<Rule> Rules { get; protected set; }

        public abstract void DoValidate();

        public abstract void AfterValidation();

        protected void setStyle(ControlInfo controlInfo)
        {
            if (controlInfo != null && controlInfo.Control != null)
            {
                controlInfo.Control.BackColor = Validity == false ? Color.IndianRed : Color.White;
            }
        }

        public bool AddRule(Rule rule)
        {
            if (rule == null)
                return false;

            Rules.Add(rule);
            return true;
        }

        public bool AddRules(Rule[] rules)
        {
            if (rules == null || rules.Length <= 0)
                return false;

            Rules.AddRange(rules);

            return true;
        }
    }
}