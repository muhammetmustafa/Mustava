using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Mustava.Ado;
using Mustava.DbManager.Dtos;
using Mustava.Extensions;

namespace Mustava.DbManager
{
    public class Services
    {
        public static List<string> GetAllDatabases(string connectionString)
        {
            const string sql = @"
                SELECT name 
                FROM sys.databases
                ORDER BY name ASC	
            ";

            return SqlHelper.Get(connectionString).Query(sql).ParseSqlRows<string>();
        }

        public static List<SqlObjectDto> GetAllObjects(string connectionString, object[] objectTypes)
        {
            const string sql = @"
                SELECT CAST(obj.name as NVARCHAR(500)) Name, obj.xtype Type
                FROM sysobjects obj
                WHERE obj.xtype IN ({0})
                ORDER BY obj.xtype, obj.name ASC
            ";

            return SqlHelper.Get(connectionString).QueryF(sql, objectTypes.ToSqlInListString()).ParseSqlRows<SqlObjectDto>();
        }
    }
}