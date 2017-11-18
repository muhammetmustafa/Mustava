using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Mustava.Ado.QueryGeneration;
using Mustava.Extensions;

namespace Mustava.Ado
{
    public class DbHelper
    {
        public SqlQuery QueryObject { get; set; }
        
        public int Timeout { get; set; }

        public string ConnectionString { get; set; }
        
        public SqlCommand GenerateSqlCommand()
        {
            if (QueryObject == null || QueryObject.GetQueryString().ExIsNullOrEmpty())
            {
                return null;
            }

            var sqlCommand = new SqlCommand(QueryObject.GetQueryString())
            {
                CommandType = QueryObject.GetCommandType(),
                CommandTimeout =  Timeout
            };

            if (!QueryObject.GetParameters().IsListNullOrEmpty())
            {
                foreach (var sqlParameter in QueryObject.GetParameters())
                {
                    sqlCommand.Parameters.Add(sqlParameter);   
                }
            }

            return sqlCommand;
        }

        
        public static DbHelper SetQuery(SqlQuery query)
        {
            return new DbHelper
            {
                QueryObject = query
            };
        }
        
        public SqlQuery Query
        {
            get
            {
                if (QueryObject == null)
                    QueryObject = new SqlQuery();
                
                return QueryObject;
            }
            set { QueryObject = value; }
        }
        
        public T FetchSingle<T>()
        {
            var sqlHelper = SqlHelper.Get(ConnectionString);
            var cmd = GenerateSqlCommand();
            if (cmd == null)
            {
                return default(T);
            }

            return sqlHelper.Query(cmd).ParseFirst<T>();
        }
        
        public List<T> FetchList<T>()
        {
            var sqlHelper = SqlHelper.Get(ConnectionString);
            var cmd = GenerateSqlCommand();
            if (cmd == null)
            {
                return default(List<T>);
            }

            return sqlHelper.Query(cmd).ParseSqlRows<T>();
        }

        public bool Execute()
        {
            var sqlHelper = SqlHelper.Get(ConnectionString);
            var cmd = GenerateSqlCommand();
            if (cmd == null)
            {
                return false;
            }

            return sqlHelper.Execute(cmd);
        }
    }
}