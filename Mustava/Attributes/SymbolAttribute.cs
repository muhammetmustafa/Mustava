using System;

namespace Mustava.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SymbolAttribute : Attribute
    {
        public string Symbol { get; set; }

        public SymbolAttribute()
        {
            
        }

        public SymbolAttribute(string symbol)
        {
            Symbol = symbol;
        }
    }
}