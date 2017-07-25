using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Mustava.Attributes;
using Mustava.Extensions;
using Mustava.Helper;

namespace Mustava.Ado
{
    public class SqlHelper
    {
        private SqlConnection _connection;

        public Action BeforeQueryAction { get; set; }

        public Action AfterQueryAction { get; set; }

        public delegate void MessageProducedDelegate(string message, string title);
        public event MessageProducedDelegate MessageProducedEvent;

        public string ConnectionString { get; set; }

        public bool BatchJob { get; set; }

        public bool DontShowExceptions { get; set; }

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
                var outputAttributes = propertyInfo.GetCustomAttributes(typeof(SqlProcOutputAttribute), false);
                if (outputAttributes.Length > 0)
                {
                    var outputAttribute = outputAttributes[0] as SqlProcOutputAttribute;
                    if (outputAttribute != null)
                    {
                        if (cmd.Parameters.Contains("@" + propertyInfo.Name))
                        {
                            propertyInfo.SetValue(paramsDto, cmd.Parameters["@" + propertyInfo.Name].Value);
                        }
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
        /// <param name="DontShowExceptions"></param>
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

        public bool Execute(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return Execute(cmd);
        }

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
                cmd.CommandTimeout = 0;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception exception)
            {
                if (!DontShowExceptions && Environment.UserInteractive)
                {
                    var title = "SQL HATASI";

                    if (cmd.Connection != null)
                    {
                        title = " (" + cmd.Connection.DataSource + "; " + cmd.Connection.Database + ")";
                    }

                    if (MessageProducedEvent != null)
                    {
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

        public bool ExecuteProc(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return ExecuteProc(cmd);
        }

        /// <summary>
        /// Execute a procedural sql command.
        /// Throws CustomException if an error occurs.
        /// </summary>
        /// <param name="cmd"></param>
        public bool ExecuteProc(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;

            return Execute(cmd);
        }

        /// <summary>
        /// Verilen Sql komutuna paramsDto parametresindeki değerlerlerden parametre oluşturarak çalıştırır
        /// ve sonucunu döndürür. Ayrıca eğer parametre dto'sunda herhangi bir parametre output olarak işaretlenmişse
        /// komut çalıştırıldıktan sonra da output değerleri parametre dto'sunda eşleşen değerlere atanır.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="paramsDto"></param>
        /// <returns></returns>
        public DataTable QueryProc(string sqlCommand, object paramsDto)
        {
            var cmd = new SqlCommand(sqlCommand);
            cmd.GenerateSqlParameters(paramsDto);

            var dataTable = QueryProc(cmd);

            //parametre dto'suna sorgudan gelen output parametrelerini ata.
            AssignSqlOutputParams(cmd, paramsDto);

            return dataTable;
        }

        public DataTable QueryProc(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return QueryProc(cmd);
        }

        /// <summary>
        /// Queries a procedural sql command. 
        /// </summary>
        /// <param name="cmd">SqlCommand to query</param>
        /// <returns>DataTable if successful, null if anything goes wrong</returns>
        public DataTable QueryProc(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;

            return Query(cmd);
        }

        public DataTable Query(string sqlCommand, params object[] parameters)
        {
            var cmd = new SqlCommand(string.Format(sqlCommand, parameters));

            return Query(cmd);
        }

        public DataTable Query(string sqlCommand)
        {
            var cmd = new SqlCommand(sqlCommand);

            return Query(cmd);
        }

        /// <summary>
        /// Queries a generic sql command. 
        /// </summary>
        /// <param name="cmd">SqlCommand to query</param>
        /// <param name="DontShowExceptions"></param>
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
                cmd.CommandTimeout = 0;

                var dsh = new DataSet();

                new SqlDataAdapter(cmd).Fill(dsh);

                return dsh.Tables.Count <= 0 ? null : dsh.Tables[0];
            }
            catch (Exception exception)
            {
                if (!DontShowExceptions && Environment.UserInteractive)
                {
                    var title = "SQL HATASI";

                    if (cmd.Connection != null)
                    {
                        title =  " (" + cmd.Connection.DataSource + "; " + cmd.Connection.Database + ")";
                    }

                    if (MessageProducedEvent != null)
                    {
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