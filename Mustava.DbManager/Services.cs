using System.Collections.Generic;
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
    }
}