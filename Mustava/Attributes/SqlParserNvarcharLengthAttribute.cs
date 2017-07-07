using System;

namespace Mustava.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlParserNvarcharLengthAttribute : System.Attribute 
    {
        public int Length { get; private set; }

        public SqlParserNvarcharLengthAttribute(int length)
        {
            Length = length;
        }
    }
}