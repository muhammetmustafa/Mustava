using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Mustava.Attributes;
using Mustava.Extensions;

namespace Mustava.Ado
{
    public static class AdoExtensions
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

        public static string ToSqlInListString(this object[] objects)
        {
            return objects.Select(i => string.Format("'{0}'", i)).Concatenate(",");
        }
        
        public static List<T> ParseSqlRows<T>(this IDataReader reader)
        {
            var list = new List<T>();

            if (reader == null)
            {
                return list;
            }

            while (reader.Read())
            {
                list.Add(ParseSqlRow<T>(reader));
            }

            if (!reader.IsClosed)
            {
                reader.Close();
            }

            return list;
        }
        
        /// <summary>
        /// Sonuç yok ise boş liste dönüyor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="useSmartPropertyNameMatching"></param>
        /// <returns></returns>
        public static List<T> ParseSqlRows<T>(this DataTable dataTable, bool useSmartPropertyNameMatching = false)
        {
            var list = new List<T>();

            if (!dataTable.HasRows())
                return list;

            if (dataTable == null || dataTable.Rows.Count <= 0)
                return list;

            list.AddRange(from DataRow dataRow in dataTable.Rows select ParseSqlRow<T>(dataRow, useSmartPropertyNameMatching));

            return list;
        }

        /// <summary>
        /// Sonuç bulamazsa null döndürür.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="useSmartPropertyNameMatching"></param>
        /// <returns></returns>
        public static T ParseFirst<T>(this DataTable dataTable, bool useSmartPropertyNameMatching = false)
        {
            return ParseFirstWithDefaultReturn(dataTable, default(T), useSmartPropertyNameMatching);
        }

        public static T ParseFirstWithEmptyReturn<T>(this DataTable dataTable, bool useSmartPropertyNameMatching = false)
        {
            return ParseFirstWithDefaultReturn(dataTable, Activator.CreateInstance<T>(), useSmartPropertyNameMatching);
        }

        public static T ParseFirstWithDefaultReturn<T>(DataTable dataTable, T returnValue, bool useSmartPropertyNameMatching = false)
        {
            if (!dataTable.HasRows())
            {
                return returnValue;
            }

            if (dataTable == null || dataTable.Rows.Count <= 0 || dataTable.Rows[0] == null)
            {
                return returnValue;
            }

            return ParseSqlRow<T>(dataTable.Rows[0], useSmartPropertyNameMatching);
        }

        public static T ParseMe<T>(this DataRow row, bool useSmartPropertyNameMatching = false)
        {
            return ParseSqlRow<T>(row, useSmartPropertyNameMatching);
        }

        public static T ParseSqlRow<T>(DataRow row, bool useSmartPropertyNameMatching = false)
        {
            if (row == null)
                return default(T);

            if (row.ItemArray.Length == 1 && AdoTypeMap.IsItAppropriateForSingleColumn(typeof(T)))
            {
                return (T)row[0];
            }

            var instance = Activator.CreateInstance<T>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var tt = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                //eğer dto'daki property dizi ise devam et. 
                if (tt.IsArray)
                {
                    var multipleColumnAttribute = propertyInfo.GetMyAttribute<SqlParserMultipleColumnAttribute>();
                    if (multipleColumnAttribute != null)
                    {
                        //We are searching for same prefixed property count. 
                        //For example if result was like this:
                        //   Result =>
                        //              Name
                        //              Surname
                        //              Prop1
                        //              Prop2
                        //              Prop3
                        //The property prefix would then be Prop. 
                        //Because you can set start number, counting would start at that property. Default = 1.
                        //In the result we would have an array of type (which is inferred from the properties)
                        //amounting 3 items. 
                        var currentColumn = multipleColumnAttribute.StartNumber;
                        var columnCount = 0;
                        while (row.Table.Columns.Contains(multipleColumnAttribute.PropertyPrefix + currentColumn))
                        {
                            currentColumn++;
                            columnCount++;
                        }

                        var arrayInstance = Activator.CreateInstance(propertyInfo.PropertyType, columnCount);
                        var array = arrayInstance as Array;
                        if (array != null)
                        {
                            for (var i = 0; i < columnCount; i++)
                            {
                                array.SetValue(AdoHelpers.SafeConvertType(row[multipleColumnAttribute.PropertyPrefix +
                                                                        (multipleColumnAttribute.StartNumber + i)],
                                    propertyInfo.PropertyType.GetElementType()), i);
                            }

                            propertyInfo.SetValue(instance, array);
                        }
                    }
                }
                else
                {
                    string column = null;
                    
                    //if attribute is used, then this is high priority.
                    var resultColumnAttribute = propertyInfo.GetMyAttribute<SqlResultColumnAttribute>();
                    if (resultColumnAttribute != null)
                    {
                        column = resultColumnAttribute.FieldName;
                    }
                    
                    //if still we dont a have a field name then search with property name
                    if (column.IsNullOrEmpty())
                    {
                        column = row.Table.Columns.Contains(propertyInfo.Name)
                            ? propertyInfo.Name
                            : null;
                    }

                    //and if still dont have a field name and if smart matching is enabled
                    //then search the property name in the result set's columns collection.
                    //But this search may not always yield the best result. Because search is done
                    //in substrings and case insensitive.
                    if (column.IsNullOrEmpty() && useSmartPropertyNameMatching)
                    {
                        var columnNameMatch = smartMatch(row, propertyInfo.Name);
                        if (columnNameMatch != null)
                        {
                            column = columnNameMatch.ColumnName;
                        }    
                    }
                    
                    //At this point just give up. Go to next property.
                    if (column == null)
                    {
                        continue;
                    }
                    
                    var value = AdoHelpers.SafeConvertType(row[column], tt);

                    propertyInfo.SetValue(instance, value);
                }
            }

            return instance;
        }
        
        public static T ParseSqlRow<T>(IDataReader row)
        {
            if (row == null)
                return default(T);

            T instance = Activator.CreateInstance<T>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                string column = null;
                
                var resultColumnAttribute = propertyInfo.GetMyAttribute<SqlResultColumnAttribute>();
                if (resultColumnAttribute != null)
                {
                    column = resultColumnAttribute.FieldName;
                }

                if (column.IsNullOrEmpty())
                {
                    column = row.ColumnExists(propertyInfo.Name) ? propertyInfo.Name : null;
                }

                if (column == null)
                {
                    continue;
                }

                var tt = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                var value = AdoHelpers.SafeConvertType(row[column], tt);

                propertyInfo.SetValue(instance, value, null);
            }

            return instance;
        }
        
        private static DataColumn smartMatch(DataRow row, string propertyName)
        {
            if (row.Table.Columns.Contains(propertyName))
                return row.Table.Columns[propertyName];

            foreach (DataColumn column in row.Table.Columns)
            {
                if (column.ColumnName.IndexOf(propertyName, StringComparison.Ordinal) >= 0 ||
                    propertyName.IndexOf(column.ColumnName, StringComparison.Ordinal) >= 0)
                    return column;
            }

            return null;
        }
        
        public static bool ColumnExists(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}