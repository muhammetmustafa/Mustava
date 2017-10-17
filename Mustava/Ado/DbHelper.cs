using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Mustava.Extensions;

namespace Mustava.Ado
{
    public class DbHelper
    {
        public Query QueryObject { get; set; }
        
        public int Timeout { get; set; }

        public string ConnectionString { get; set; }
        
        public SqlCommand GenerateSqlCommand()
        {
            if (QueryObject == null || QueryObject.QueryString.ExIsNullOrEmpty())
            {
                return null;
            }

            var sqlCommand = new SqlCommand(QueryObject.QueryString)
            {
                CommandType = QueryObject.CommandType,
                CommandTimeout =  Timeout
            };

            if (!QueryObject.Parameters.IsListNullOrEmpty())
            {
                foreach (var sqlParameter in QueryObject.Parameters)
                {
                    sqlCommand.Parameters.Add(sqlParameter);   
                }
            }

            return sqlCommand;
        }

        public static DbHelper SetQuery(Query query)
        {
            return new DbHelper
            {
                QueryObject = query
            };
        }

        public T FetchItem<T>()
        {
            var sqlHelper = SqlHelper.Get(ConnectionString);
            sqlHelper.Timeout = Timeout;
            sqlHelper
            
        }
        
        public List<T> FetchList<T>()
        {
            
        }

        public bool Execute()
        {
            
        }
    }
}