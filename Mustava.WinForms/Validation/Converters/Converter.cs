namespace Mustava.WinForms.Validation.Converters
{
    public abstract class Converter
    {
        public virtual object Convert(object obj)
        {
            return doConvert(obj);
        }

        protected abstract object doConvert(object obj);         
    }
}