using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Mustava.Attributes;
using Mustava.Extensions;

namespace Mustava.Ado
{
    public static class SqlExtensions
    {
        /// <summary>
        /// Bir SqlCommand'ın parametlerine object nesnesindeki bütün özelliklerin (property) 
        /// listesini parametre olarak ekler. Dolayısıyla object nesnesindeki bütün özelliklerin 
        /// karşılıkları SqlCommand'da parametre olarak kullanılabilir olması gerekmektedir.
        /// 
        /// MSL:
        /// 
        ///     obj =>   obj.StartDate, obj.EndDate
        ///     SqlCommand => AddWithValue("@StartDate", obj.StartDate);
        ///                   AddWithValue("@EndDate", obj.EndDate);
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="obj"></param>
        public static void GenerateSqlParameters(this IDbCommand cmd, object obj)
        {
            if (cmd == null || obj == null)
                return;

            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                var parameterName = "@" + propertyInfo.Name;
                int? parameterLength = 0;

                var nvarcharLengthAttributes = propertyInfo.GetCustomAttributes(typeof(SqlParserNvarcharLengthAttribute), false);
                if (nvarcharLengthAttributes.Length > 0)
                {
                    var nvarcharLengthAttribute = nvarcharLengthAttributes[0] as SqlParserNvarcharLengthAttribute;
                    if (nvarcharLengthAttribute != null)
                    {
                        parameterLength = nvarcharLengthAttribute.Length;
                    }
                }

                var outputAttributes = propertyInfo.GetCustomAttributes(typeof(SqlProcOutputAttribute), false);
                if (outputAttributes.Length > 0)
                {
                    var outputAttribute = outputAttributes[0] as SqlProcOutputAttribute;
                    if (outputAttribute != null)
                    {
                        cmd.NewOutputParameter(parameterName, outputAttribute.SqlDbType, parameterLength.GetValueOrDefault(0));
                        continue;
                    }
                }

                cmd.NewInputParameter(parameterName, propertyInfo.GetValue(obj, null), parameterLength);
            }
        }

        public static SqlParameter NewInputParameter(this IDbCommand cmd, string parameterName, object value)
        {
            return cmd.NewInputParameter(parameterName, value, null);
        }

        public static SqlParameter NewInputParameter(this IDbCommand cmd, string parameterName, object value, int? size)
        {
            if (cmd == null || parameterName.Equals(string.Empty))
                return null;

            if (!parameterName.StartsWith("@"))
                parameterName = "@" + parameterName;

            if (value == null)
            {
                var p = new SqlParameter(parameterName, DBNull.Value);
                cmd.Parameters.Add(p);
                return p;
            }


            var inputParameter = new SqlParameter(parameterName, AdoTypeMap.GetAdoType(value.GetType()))
            {
                Value = value
            };

            if (size != null && size > 0)
                inputParameter.Size = (int)size;

            cmd.Parameters.Add(inputParameter);

            return inputParameter;
        }

        public static SqlParameter NewOutputParameter(this IDbCommand cmd, string parameterName, SqlDbType type)
        {
            return cmd.NewOutputParameter(parameterName, type, 0);
        }

        public static SqlParameter NewOutputParameter(this IDbCommand cmd, string parameterName, SqlDbType type, int size)
        {
            if (cmd == null || parameterName.Equals(string.Empty))
                return null;

            if (parameterName.StartsWith("@"))
                parameterName = parameterName.Substring(1);

            var outputParameter = new SqlParameter("@" + parameterName, type, size) { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputParameter);

            return outputParameter;
        }

        public static bool HasRows(this DataTable dataTable)
        {
            return dataTable != null && dataTable.Rows.Count > 0;
        }

        public static string ToInString(this object[] objects)
        {
            return objects.Select(i => string.Format("'{0}'", i)).Concatenate(",");
        }
    }
}