using System;
using Mustava.Validation.Base;

namespace Mustava.Validation.Rules
{
    public class CompareRule : TwoControlRule
    {
        public enum ComparasionOperator { LessThan, LessThanEqualsTo, EqualsTo, GreaterThan, GreaterThanEqualsTo}

        public ComparasionOperator Operator { get; set; }

        protected override bool CustomValidation()
        {
            var first = ControlInfo1.GetValue() as IComparable;
            if (first == null)
                return false;

            var second = ControlInfo2.GetValue() as IComparable;
            if (second == null)
                return false;

            switch (Operator)
            {
                case ComparasionOperator.LessThan:
                    return first.CompareTo(second) < 0;
                case ComparasionOperator.LessThanEqualsTo:
                    return first.CompareTo(second) <= 0;
                case ComparasionOperator.EqualsTo:
                    return first.CompareTo(second) == 0;
                case ComparasionOperator.GreaterThan:
                    return first.CompareTo(second) > 0;
                case ComparasionOperator.GreaterThanEqualsTo:
                    return first.CompareTo(second) >= 0;
                default:
                    return false;
            }
        }
    }
}