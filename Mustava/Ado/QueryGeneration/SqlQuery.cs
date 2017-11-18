using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mustava.Ado.QueryGeneration
{
    public class SqlQuery : IQuery
    {
        private readonly CommandType _commandType;
        private string _queryString;
        private List<SqlParameter> _parameters;

        public SqlQuery()
        {            
            _commandType = CommandType.Text;
        }

        public DbHelper DbHelper
        {
            get
            {
                return new DbHelper(){QueryObject = this};
            }
        }

        protected SqlQuery(CommandType commandType)
        {
            _commandType = commandType;
        }

        public static SqlQuery QueryString(string queryString)
        {
            return New(queryString, null);
        }
        
        public static SqlQuery New(string queryString, List<SqlParameter> parameters)
        {
            return new SqlQuery()
            {
                _queryString = queryString,
                _parameters = parameters
            };
        }

        public SqlQuery SetQueryString(string queryString)
        {
            this._queryString = queryString;

            return this;
        }

        public SqlQuery Parameters(object parametersObject)
        {
            this._parameters = AdoExtensions.GenerateSqlParameters(null, parametersObject);
            
            return this;
        }

        public SqlQuery FormatParameters(params object[] parameters)
        {
            this._queryString = string.Format(this._queryString, parameters);

            return this;
        }

        public DbHelper GetDbHelper()
        {
            return new DbHelper(){QueryObject = this};
        }

        public DbHelper GetDbHelper(string connectionString)
        {
            return new DbHelper(){QueryObject = this, 
                ConnectionString = connectionString};
        }
        
        public CommandType GetCommandType()
        {
            return _commandType;
        }

        public List<SqlParameter> GetParameters()
        {
            return _parameters;
        }

        public string GetQueryString()
        {
            return _queryString;
        }
    }
}