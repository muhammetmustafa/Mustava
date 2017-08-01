using System;

namespace Mustava.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlResultColumnAttribute : Attribute
    {
        public string FieldName { get; set; }

        public SqlResultColumnAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}