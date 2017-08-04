using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Mustava.Attributes;
using Mustava.Extensions;
using Mustava.Helpers;

namespace Mustava.Ado
{
    public class SqlHelper
    {
        private SqlConnection _connection;

        public delegate void MessageProducedDelegate(string message, string title);
        public event MessageProducedDelegate MessageProducedEvent;

        public Action BeforeQueryAction { get; set; }

        public Action AfterQueryAction { get; set; }

        public string ConnectionString { get; set; }

        public bool BatchJob { get; set; }

        public bool DontShowExceptions { get; set; }

        public int Timeout { get; set; }

        public bool ShowSql { get; set; }

        public static SqlHelper Get()
        {
            return new SqlHelper();
        }

        public static SqlHelper Get(string connectionString)
        {
            return new SqlHelper {ConnectionString = connectionString};
        }

        private void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(ConnectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// parametre dto'sunun property'leriyle sql komutunun output parametreleri arasında bağlantı kurulmuşsa
        /// komut çalıştırıldıktan sonra bu dto'nun eşleşen property'lerine sql komutundan çalıştırılmasından gelen 
        /// output değerleri atayalım.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramsDto"></param>
        private void AssignSqlOutputParams(SqlCommand cmd, object paramsDto)
        {
            foreach (var propertyInfo in paramsDto.GetType().GetProperties())
            {
                var outputAttribute = propertyInfo.GetMyAttribute<SqlProcOutputAttribute>();
                if (outputAttribute != null)
                {
                    if (cmd.Parameters.Contains("@" + propertyInfo.Name))
                    {
                        propertyInfo.SetValue(paramsDto, cmd.Parameters["@" + propertyInfo.Name].Value);
                    }
                }
            }
        }

        /// <summary>
        /// Verilen Sql komutuna paramsDto parametresindeki değerlerlerden parametre oluşturarak çalıştırır
        /// ve sonucunu döndürür. Ayrıca eğer parametre dto'sunda herhangi bir parametre output olarak işaretlenmişse
        /// komut çalıştırıldıktan sonra da output değerleri parametre dto'sunda eşleşen değerlere atanır.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="paramsDto"></param>
        /// <returns></returns>
        public bool ExecuteProc(string sqlCommand, object paramsDto)
        {
            var cmd = new SqlCommand(sqlCommand);
            cmd.GenerateSqlParameters(paramsDto);

            var dataTable = ExecuteProc(cmd);

            //parametre dto'suna sorgudan gelen output parametrelerini ata.
            AssignSqlOutputParams(cmd, paramsDto);

            return dataTable;
        }

        /// <summary>
        /// Executes a parameterless procedure command. It's parameters must be supplied beforehand.
        /// </summary>
        /// <param name="sqlCommand">Procedure name to execute</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public bool ExecuteProc(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return ExecuteProc(cmd);
        }

        /// <summary>
        /// Executes a procedure command. It's parameters must be supplied beforehand.
        /// </summary>
        /// <param name="cmd">SqlCommand object to execute.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public bool ExecuteProc(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;

            return Execute(cmd);
        }

        /// <summary>
        /// Formats the sql with the parameters provided and executes the sql.
        /// </summary>
        /// <param name="sqlCommand">Parameterized sql string appropriate for string.Format() method.</param>
        /// <param name="parameters">Parameter list to be supplied to string.Format() method. Must match the first argument.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ExecuteF(string sqlCommand, params object[] parameters)
        {
            var cmd = new SqlCommand(string.Format(sqlCommand, parameters));

            return Execute(cmd);
        }

        /// <summary>
        /// Executes the sql with the parameters provided by the parameters object and executes the sql.
        /// </summary>
        /// <param name="sqlCommand">Parameterized sql string appropriate for SqlParameters.</param>
        /// <param name="parameters">Object from which parameters will be extracted and injected into SqlCommand object.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ExecuteP(string sqlCommand, object parameters)
        {
            var cmd = new SqlCommand(sqlCommand);
            cmd.GenerateSqlParameters(parameters);

            return Execute(cmd);
        }

        /// <summary>
        /// Executes a sql text.
        /// </summary>
        /// <param name="sqlCommand">Sql text to execute.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Execute(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return Execute(cmd);
        }

        /// <summary>
        /// Executes a SqlCommand object.
        /// </summary>
        /// <param name="cmd">SqlCommand object to execute.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Execute(SqlCommand cmd)
        {
            try
            {
                if (!BatchJob && Environment.UserInteractive)
                {
                    if (BeforeQueryAction != null)
                    {
                        BeforeQueryAction.Invoke();
                    }
                }

                OpenConnection();

                cmd.Connection = _connection;
                cmd.CommandTimeout = Timeout;

                if (ShowSql)
                {
                    Debug.WriteLine(cmd.CommandText);
                }

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception exception)
            {
                if (!DontShowExceptions && Environment.UserInteractive)
                {
                    if (MessageProducedEvent != null)
                    {
                        var title = "SQL HATASI";

                        if (cmd.Connection != null)
                        {
                            title = " (" + cmd.Connection.DataSource + "; " + cmd.Connection.Database + ")";
                        }

                        MessageProducedEvent.Invoke(exception.Message, title);
                    }
                }
                
                LogHelper.Log("Exception: " + exception.Message);
                LogHelper.Log(exception.GetAllInnerExceptions().Select(exc => exc.Message).Concatenate("\n") + "\n" + exception.StackTrace);

                return false;
            }
            finally
            {
                if (!BatchJob && Environment.UserInteractive)
                {
                    if (AfterQueryAction != null)
                    {
                        AfterQueryAction.Invoke();
                    }
                }

                CloseConnection();
            }
        }

        /// <summary>
        /// Executes a procedure with parameters inferred from another object.
        /// First a sql command is initialized. And its parameters are inferred from second argument. 
        /// That's why parameter names and types of the procedure and property names and types of the 
        /// object must match.
        /// </summary>
        /// <param name="sqlCommand">Procedure to be executed.</param>
        /// <param name="paramsDto">Object which prodecure parameters and values are inferred from.</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable QueryProc(string sqlCommand, object paramsDto)
        {
            var cmd = new SqlCommand(sqlCommand);
            cmd.GenerateSqlParameters(paramsDto);

            var dataTable = QueryProc(cmd);

            //parametre dto'suna sorgudan gelen output parametrelerini ata.
            AssignSqlOutputParams(cmd, paramsDto);

            return dataTable;
        }

        /// <summary>
        /// Executes a procedure without any parameters. So only procedure name is required.
        /// </summary>
        /// <param name="sqlCommand">Procedure name to exectue</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable QueryProc(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return QueryProc(cmd);
        }

        /// <summary>
        /// Executes a procedure SqlCommand object. Assuming parameters provided beforehand.
        /// </summary>
        /// <param name="cmd">Procedure to execute</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable QueryProc(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;

            return Query(cmd);
        }

        /// <summary>
        /// Formats the sql with the parameters provided and executes the sql.
        /// </summary>
        /// <param name="sqlCommand">Parameterized sql string appropriate for string.Format() method.</param>
        /// <param name="parameters">Parameter list to be supplied to string.Format() method. Must match the first argument.</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        [Obsolete("Use QueryF() istead")]
        public DataTable Query(string sqlCommand, params object[] parameters)
        {
            var cmd = new SqlCommand(string.Format(sqlCommand, parameters));

            return Query(cmd);
        }

        /// <summary>
        /// Executes the sql with the parameters provided by the parameters object and executes the sql.
        /// </summary>
        /// <param name="sqlCommand">Parameterized sql string appropriate for SqlParameters.</param>
        /// <param name="parameters">Object from which parameters will be extracted and injected into SqlCommand object.</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable QueryP(string sqlCommand, object parameters)
        {
            var cmd = new SqlCommand(sqlCommand);
            cmd.GenerateSqlParameters(parameters);

            return Query(cmd);
        }

        /// <summary>
        /// Formats the sql with the parameters provided and executes the sql.
        /// </summary>
        /// <param name="sqlCommand">Parameterized sql string appropriate for string.Format() method.</param>
        /// <param name="parameters">Parameter list to be supplied to string.Format() method. Must match the first argument.</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable QueryF(string sqlCommand, params object[] parameters)
        {
            var cmd = new SqlCommand(string.Format(sqlCommand, parameters));

            return Query(cmd);
        }

        /// <summary>
        /// Simply executes the sql string.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable Query(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return Query(cmd);
        }

        /// <summary>
        /// Queries a generic sql command. 
        /// </summary>
        /// <param name="cmd">SqlCommand to query</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable Query(SqlCommand cmd)
        {
            try
            {
                if (!BatchJob && Environment.UserInteractive)
                {
                    if (BeforeQueryAction != null)
                    {
                        BeforeQueryAction.Invoke();
                    }
                }

                OpenConnection();

                cmd.Connection = _connection;
                cmd.CommandTimeout = Timeout;

                var dsh = new DataSet();

                if (ShowSql)
                {
                    Debug.WriteLine(cmd.CommandText);
                }

                new SqlDataAdapter(cmd).Fill(dsh);

                return dsh.Tables.Count <= 0 ? null : dsh.Tables[0];
            }
            catch (Exception exception)
            {
                if (!DontShowExceptions && Environment.UserInteractive)
                {
                    if (MessageProducedEvent != null)
                    {
                        var title = "SQL HATASI";

                        if (cmd.Connection != null)
                        {
                            title = " (" + cmd.Connection.DataSource + "; " + cmd.Connection.Database + ")";
                        }

                        MessageProducedEvent.Invoke(exception.Message, title);
                    }
                }

                return null;
            }
            finally
            {
                if (!BatchJob)
                {
                    if (AfterQueryAction != null)
                    {
                        AfterQueryAction.Invoke();
                    }
                }

                CloseConnection();
            }
        }
    }
}