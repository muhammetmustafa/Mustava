using System;
using System.Collections.Generic;
using System.ComponentModel;
using Mustava.Ado;
using Mustava.Helper;

namespace Mustava.DbManager
{
    public class Services
    {
        public static List<string> GetAllDatabases(string connectionString)
        {
            var sql = @"
                SELECT name 
                FROM sys.databases
                ORDER BY name ASC	
            ";

            return SqlHelper.Get(connectionString).Query(sql).ParseSqlRows<string>();
        }

        public static List<string> GetAllTables(string connectionString)
        {
            var sql = @"
                SELECT TABLE_NAME 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_TYPE = 'BASE TABLE'
                ORDER BY TABLE_NAME ASC
            ";

            return SqlHelper.Get(connectionString).Query(sql).ParseSqlRows<string>();
        }

        public static List<string> GetAllTables(string connectionString)
        {
            var sql = @"
                SELECT TABLE_NAME 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_TYPE = 'BASE TABLE'
                ORDER BY TABLE_NAME ASC
            ";

            return SqlHelper.Get(connectionString).Query(sql).ParseSqlRows<string>();
        }
    }
}