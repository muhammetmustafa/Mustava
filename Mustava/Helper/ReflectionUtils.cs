using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Mustava.Ado;
using Mustava.Attributes;
using Mustava.Extensions;

namespace Mustava.Helper
{
    public static class ReflectionUtils
    {
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
                    var member = typeof(T).GetMember(propertyInfo.Name);
                    if (member.Length > 0)
                    {
                        var attributes = member[0].GetCustomAttributes(typeof(SqlParserMultipleColumnAttribute), false);
                        if (attributes.Length > 0)
                        {
                            var multipleColumnAttribute = attributes[0] as SqlParserMultipleColumnAttribute;
                            if (multipleColumnAttribute != null)
                            {
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
                                        array.SetValue(SafeConvertType(row[multipleColumnAttribute.PropertyPrefix +
                                                                                (multipleColumnAttribute.StartNumber + i)],
                                            propertyInfo.PropertyType.GetElementType()), i);
                                    }

                                    propertyInfo.SetValue(instance, array);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //veritabanı tablosundan gelen sütun ismiyle
                    //dönüştürülecek dto'daki property isimlerini eşleştir.
                    var column = useSmartPropertyNameMatching
                        ? smartMatch(row, propertyInfo.Name)
                        : row.Table.Columns.Contains(propertyInfo.Name) ? row.Table.Columns[propertyInfo.Name] : null;
                    if (column == null)
                    {
                        continue;
                    }

                    dynamic value = SafeConvertType(row[column], tt);

                    propertyInfo.SetValue(instance, value);
                }
            }

            return instance;
        }

        private static dynamic SafeConvertType(object obj, Type type)
        {
            if (obj == DBNull.Value || obj == null)
            {
                return null;
            }

            return Convert.ChangeType(obj, type);
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

        public static List<object> GetValuesOfProperty<T>(this List<T> objs, string propertyName,
            bool recursive = false)
        {
            if (objs == null || propertyName.IsNullOrEmpty())
                return null;
            if (objs.Count <= 0)
                return new List<object>();

            return objs.Select(obj => obj.GetValueOfProperty(propertyName, recursive)).ToList();
        }

        public static object GetValueOfProperty(this object obj, string propertyName, bool recursive = false)
        {
            if (recursive)
            {
                return GetValueOfPropertyRecursive(obj, propertyName);
            }

            if (obj == null)
            {
                return null;
            }

            var info = obj.GetType().GetProperty(propertyName);

            return info == null ? null : info.GetValue(obj);
        }

        public static object GetValueOfPropertyRecursive(this object obj, string propertyName)
        {
            if (obj == null)
            {
                return null;
            }

            var lastValue = obj;
            var properties = propertyName.Split('.');
            foreach (var property in properties)
            {
                var propertyInfo = lastValue.GetType().GetProperty(property);
                if (propertyInfo == null)
                    return null;

                lastValue = propertyInfo.GetValue(lastValue);
            }

            return lastValue;
        }

        public static string GetStringProperty(this object obj, string propertyName)
        {
            return obj.GetValueOfProperty(propertyName).ToStringOrEmpty();
        }

        public static DateTime GetDateTimeProperty(this object obj, string propertyName)
        {
            return obj.GetStringProperty(propertyName).ToDateTimeX(DateTime.MinValue);
        }

        public static List<string> GetPropertyNames(this object obj)
        {
            var list = new List<string>();

            foreach (var propertyInfo in obj.GetType().GetProperties().OrderBy(p => p.MetadataToken))
            {
                list.Add(propertyInfo.Name);
            }

            return list;
        }

        public static bool HasProperty(this object obj, string propertyName)
        {
            if (obj.GetType().GetProperties().Any(propertyInfo => propertyInfo.Name.Equals(propertyName)))
            {
                return true;
            }

            return false;
        }

        public static void ResetObject(this object obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(obj, GetDefault(propertyInfo.PropertyType));
                }
            }
        }

        public static void ResetObject(this object obj, string[] leftOutPropertyNames)
        {
            if (leftOutPropertyNames == null || leftOutPropertyNames.Length <= 0)
            {
                ResetObject(obj);
                return;
            }

            foreach (var propertyInfo in obj.GetType().GetProperties().Where(p => !leftOutPropertyNames.Contains(p.Name)))
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(obj, GetDefault(propertyInfo.PropertyType));
                }
            }
        }

        private static object GetDefault(Type type)
        {
            if (type == typeof(string))
                return string.Empty;

            if (type == typeof(int) || type == typeof(long))
                return 0;

            if (type == typeof(DateTime))
                return DateTime.MinValue;

            return null;
        }

        /// <summary>
        /// C# 6.0 'date nameof operatörünün işini yapar. field'ın ismini öğrenmek için kullanıyoruz.
        /// 
        /// MyClass >> int Field;
        /// 
        /// GetMemberName((MyClass myClass) => myClass.Field)
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="memberAccess"></param>
        /// <returns></returns>
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        public static void SetAllMyMembersForIEditable<T>(this T target, T source)
        {
            target.SetAllMyMembers(source, new[] { "" });
        }

        public static void SetAllMyMembers<T>(this T target, T source)
        {
            target.SetAllMyMembers(source, new string[] { });
        }

        public static void SetAllMyMembers<T>(this T target, T source, string[] leftOutPropertyNames)
        {
            if (target == null || source == null)
                return;

            foreach (var propertyInfo in source.GetType().GetProperties().Where(p => !leftOutPropertyNames.Contains(p.Name)))
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(target, source.GetValueOfProperty(propertyInfo.Name));
                }
            }
        }
    }
}