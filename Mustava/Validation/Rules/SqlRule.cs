using System.Data.SqlClient;
using Mustava.Ado;
using Mustava.Helper;
using Mustava.Validation.Base;

namespace Mustava.Validation.Rules
{
    public class SqlRule : SingleControlRule
    {
        public string CommandText { get; set; }

        protected override bool CustomValidation()
        {
            var val = ControlInfo.GetValue().ToStringOrEmpty();

            if (val.IsEmpty())
                return true;

            if (CommandText == null)
                return false;

            var sql = string.Format(CommandText, val.ToStringOrEmpty());
            var cmd = new SqlCommand(sql);
            
            return new SqlHelper().Query(cmd).Rows.Count > 0;
        }
    }
}