using Mustava.WinForms.Validation.Rules;

namespace Mustava.WinForms.Validation
{
    public static class RuleSelector
    {
        private static NonNullOrEmptyRule _nonNullOrEmptyRule;
        private static StartEndRule _startEndRule;
        private static SqlRule _sqlRule;
        private static CompareRule _compareRule;

        public static NonNullOrEmptyRule NonNullOrEmptyRule
        {
            get
            {
                return _nonNullOrEmptyRule ?? (_nonNullOrEmptyRule = new NonNullOrEmptyRule());
            }
        }

        public static StartEndRule StartEndRule
        {
            get
            {
                return _startEndRule ?? (_startEndRule = new StartEndRule());
            }
        }

        public static SqlRule SqlRule
        {
            get
            {
                return _sqlRule ?? (_sqlRule = new SqlRule());
            }
        }

        public static CompareRule CompareRule
        {
            get
            {
                return _compareRule ?? (_compareRule = new CompareRule());
            }
        }
    }
}