using System;

namespace Mustava.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlParserMultipleColumnAttribute : Attribute
    {
        public string PropertyPrefix { get; set; }

        public int StartNumber { get; set; }
    }
}