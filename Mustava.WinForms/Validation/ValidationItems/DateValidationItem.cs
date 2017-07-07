using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mustava.Extensions;
using Mustava.WinForms.Validation.Base;
using Mustava.WinForms.Validation.Utils;

namespace Mustava.WinForms.Validation.ValidationItems
{
    public class DateValidationItem : BaseValidationItem
    {
        public ControlInfo FirstYearPart { get; set; }

        public ControlInfo FirstMonthPart { get; set; }

        public ControlInfo FirstDayPart { get; set; }

        public ControlInfo SecondYearPart { get; set; }
        
        public ControlInfo SecondMonthPart { get; set; }

        public ControlInfo SecondDayPart { get; set; }

        public DateValidationItem()
        {
            Rules = new List<Rule>();
        }

        public void SetControlInfos(Control firstYear, Control firstMonth, Control secondYear, Control secondMonth,
            string propertyName)
        {
            FirstYearPart = new ControlInfo(firstYear, propertyName);
            FirstMonthPart = new ControlInfo(firstMonth, propertyName);
            SecondYearPart = new ControlInfo(secondYear, propertyName);
            SecondMonthPart = new ControlInfo(secondMonth, propertyName);
        }

        public void SetControlInfos(ControlInfo firstYear, ControlInfo firstMonth, ControlInfo secondYear, ControlInfo secondMonth)
        {
            FirstYearPart = firstYear;
            FirstMonthPart = firstMonth;
            SecondYearPart = secondYear;
            SecondMonthPart = secondMonth;
        }

        public override void DoValidate()
        {
            var first = getFirstDate();
            var second = getSecondDate();

            Validity = first <= second;

            AfterValidation();
        }

        public override void AfterValidation()
        {
            setStyle(FirstYearPart);
            setStyle(FirstMonthPart);
            setStyle(FirstDayPart);
            setStyle(SecondYearPart);
            setStyle(SecondMonthPart);
            setStyle(SecondDayPart);
        }

        private DateTime getFirstDate()
        {
            var year = FirstYearPart == null ? 1900 : FirstYearPart.GetValue().ToInt();
            var month = FirstMonthPart == null ? 1 : FirstMonthPart.GetValue().ToInt();
            var day = FirstDayPart == null ? 1 : FirstDayPart.GetValue().ToInt();

            return new DateTime(year, month, day, 0, 0, 0);
        }

        private DateTime getSecondDate()
        {
            var year = SecondYearPart == null ? 1900 : SecondYearPart.GetValue().ToInt();
            var month = SecondMonthPart == null ? 1 : SecondMonthPart.GetValue().ToInt();
            var day = SecondDayPart == null ? 1 : SecondDayPart.GetValue().ToInt();

            return new DateTime(year, month, day, 0, 0, 0);
        }
    }
}