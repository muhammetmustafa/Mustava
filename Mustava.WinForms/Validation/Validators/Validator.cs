using System.Collections.Generic;
using System.Windows.Forms;
using Mustava.Extensions;
using Mustava.WinForms.Validation.Base;
using Mustava.WinForms.Validation.Utils;
using Mustava.WinForms.Validation.ValidationItems;

namespace Mustava.WinForms.Validation.Validators
{
    public class Validator
    {
        private readonly List<BaseValidationItem> _validationItems = new List<BaseValidationItem>();
        private readonly List<Control> _invalidControls = new List<Control>(); 

        public bool AddValidation(Control control, string propertyName, params Rule[] rules)
        {
            if (control == null || rules.Length <= 0)
                return false;

            var one = new ControlInfo(control, propertyName);
            var validationItem = new ValidationItem();
            validationItem.AddControlInfo(one);
            validationItem.AddRules(rules);
            _validationItems.Add(validationItem);

            return true;
        }

        public bool AddValidation(Control control1, Control control2, string propertyName, params Rule[] rules)
        {
            if (control1 == null || control2 == null || propertyName.ExIsNullOrEmpty() || rules.Length <= 0)
                return false;

            var one = new ControlInfo(control1, propertyName);
            var two = new ControlInfo(control2, propertyName);
            
            var validationItem = new ValidationItem();
            validationItem.AddControlInfo(one);
            validationItem.AddControlInfo(two);
            validationItem.AddRules(rules);

            _validationItems.Add(validationItem);

            return true;
        }

        public bool AddValidation(ControlInfo control1, ControlInfo control2, params Rule[] rules)
        {
            if (control1 == null || control2 == null || rules.Length <= 0)
                return false;

            var validationItem = new ValidationItem();
            validationItem.AddControlInfo(control1);
            validationItem.AddControlInfo(control2);
            validationItem.AddRules(rules);

            _validationItems.Add(validationItem);

            return true;
        }

        public bool DateValidationItem(Control firstYear, Control firstMonth, Control secondYear, Control secondMonth,
            string propertyName)
        {
            var validationItem = new DateValidationItem();
            validationItem.SetControlInfos(firstYear, firstMonth, secondYear, secondMonth, propertyName);

            _validationItems.Add(validationItem);

            return true;
        }

        public bool DateValidationItem(ControlInfo firstYear, ControlInfo firstMonth, ControlInfo secondYear, ControlInfo secondMonth)
        {
            var validationItem = new DateValidationItem();
            validationItem.SetControlInfos(firstYear, firstMonth, secondYear, secondMonth);

            _validationItems.Add(validationItem);

            return true;
        }

        public bool Validate()
        {
            var result = true;

            _invalidControls.Clear();

            foreach (var validationItem in _validationItems)
            {
                validationItem.DoValidate();

                result &= validationItem.Validity;

                if (validationItem.Validity == false)
                {
                    _invalidControls.AddRange(validationItem.InvalidControls);
                }
            }

            if (result == false || _invalidControls.Count > 0)
            {
                _invalidControls[0].Focus();

                var textBased = _invalidControls[0] as TextBoxBase;
                if (textBased != null)
                    textBased.SelectAll();

                var selectAllMethod = _invalidControls[0].GetType().GetMethod("SelectAll");
                if (selectAllMethod != null)
                {
                    selectAllMethod.Invoke(_invalidControls[0], new object[]{});
                }
            }

            return result;
        }
    }
}