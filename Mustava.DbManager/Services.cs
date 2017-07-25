using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Mustava.Ado;
using Mustava.DbManager.Dtos;
using Mustava.Extensions;
using Mustava.Helper;

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

        public static List<NameDto> GetAllObjects(string connectionString, object[] objectTypes)
        {
            const string sql = @"
                SELECT CAST(name as NVARCHAR(500)) Name
                FROM sysobjects 
                WHERE sys.sysobjects.xtype IN ({0})
                ORDER BY name ASC
            ";

            return SqlHelper.Get(connectionString).Query(sql, objectTypes.ToInString()).ParseSqlRows<NameDto>();
        }
    }
}