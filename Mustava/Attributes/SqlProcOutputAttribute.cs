using System;
using System.Data;

namespace Mustava.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SqlProcOutputAttribute : Attribute
    {
        public SqlDbType SqlDbType { get; set; }

        public SqlProcOutputAttribute(SqlDbType sqlDbType)
        {
            SqlDbType = sqlDbType;
        }
    }
}