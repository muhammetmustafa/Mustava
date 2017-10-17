using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mustava.Ado
{
    public class Query
    {
        public string QueryString { get; set; }
        
        public CommandType CommandType { get; set; }

        public List<SqlParameter> Parameters { get; set; }

        public static Query New()
        {
            return New(null);
        }
        
        public static Query New(string queryString)
        {
            return New(queryString, CommandType.Text);
        }
        
        public static Query New(string queryString, CommandType commandType)
        {
            return New(queryString, commandType, null);
        }
        
        public static Query New(string queryString, CommandType commandType, List<SqlParameter> parameters)
        {
            return new Query()
            {
                QueryString = queryString, 
                CommandType = commandType,
                Parameters = parameters
            };
        }

        public Query SetQueryString(string queryString)
        {
            this.QueryString = queryString;

            return this;
        }

        public Query IsProcedure()
        {
            this.CommandType = CommandType.StoredProcedure;

            return this;
        }

        public Query IsNotProcedure()
        {
            this.CommandType = CommandType.Text;

            return this;
        }

        public Query SetParameters(object parametersObject)
        {
            this.Parameters = AdoExtensions.GenerateSqlParameters(null, parametersObject);
            
            return this;
        }

        public Query SetFormatParameters(params object[] parameters)
        {
            this.QueryString = string.Format(this.QueryString, parameters);

            return this;
        }
        
        
    }
}