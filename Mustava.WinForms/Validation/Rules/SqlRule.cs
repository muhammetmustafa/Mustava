﻿using System.Data.SqlClient;
using Mustava.Ado;
using Mustava.Extensions;
using Mustava.WinForms.Validation.Base;

namespace Mustava.WinForms.Validation.Rules
{
    public class SqlRule : SingleControlRule
    {
        public string CommandText { get; set; }

        protected override bool CustomValidation()
        {
            var val = ControlInfo.GetValue().ExToStringOrEmpty();

            if (val.ExIsEmpty())
                return true;

            if (CommandText == null)
                return false;

            var sql = string.Format(CommandText, val.ExToStringOrEmpty());
            var cmd = new SqlCommand(sql);
            
            return new SqlHelper().Query(cmd).Rows.Count > 0;
        }
    }
}