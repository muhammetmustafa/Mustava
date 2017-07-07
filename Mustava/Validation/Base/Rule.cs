namespace Mustava.Validation.Base
{
    public abstract class Rule
    {
        public abstract bool Validate();

        protected abstract bool CustomValidation();
    }
}
