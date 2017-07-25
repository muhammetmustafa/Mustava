using System;
using System.Collections.Generic;
using System.Data;

namespace Mustava.Ado
{
    public class AdoTypeMap
    {
        private static readonly Dictionary<Type, SqlDbType> _typeMap;
        private static readonly Dictionary<Type, bool> _appropriateTypesForSingleColumnResult; 

        static AdoTypeMap()
        {
            _typeMap = new Dictionary<Type, SqlDbType>();
            _appropriateTypesForSingleColumnResult = new Dictionary<Type, bool>();

            InitDbTypeMap();
            InitAppTypeMap();
        }

        public static void InitDbTypeMap()
        {
            _typeMap[typeof(string)] = SqlDbType.NVarChar;
            _typeMap[typeof(char[])] = SqlDbType.NVarChar;
            _typeMap[typeof(byte)] = SqlDbType.TinyInt;
            _typeMap[typeof(short)] = SqlDbType.SmallInt;
            _typeMap[typeof(int)] = SqlDbType.Int;
            _typeMap[typeof(long)] = SqlDbType.BigInt;
            _typeMap[typeof(byte[])] = SqlDbType.Image;
            _typeMap[typeof(bool)] = SqlDbType.Bit;
            _typeMap[typeof(DateTime)] = SqlDbType.DateTime2;
            _typeMap[typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset;
            _typeMap[typeof(decimal)] = SqlDbType.Money;
            _typeMap[typeof(float)] = SqlDbType.Real;
            _typeMap[typeof(double)] = SqlDbType.Float;
            _typeMap[typeof(TimeSpan)] = SqlDbType.Time;
            _typeMap[typeof(Guid)] = SqlDbType.UniqueIdentifier;
        }

        public static void InitAppTypeMap()
        {
            _appropriateTypesForSingleColumnResult.Add(typeof(string), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(char[]), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(byte[]), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(TimeSpan), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(TimeSpan?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(Guid), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(Guid?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(bool), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(bool?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(int), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(int?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(long), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(long?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(byte), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(byte?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(short), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(short?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(decimal), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(decimal?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(float), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(float?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(double), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(double?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(DateTime), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(DateTime?), true);

            _appropriateTypesForSingleColumnResult.Add(typeof(DateTimeOffset), true);
            _appropriateTypesForSingleColumnResult.Add(typeof(DateTimeOffset?), true);
        }

        public static SqlDbType GetAdoType(Type type)
        {
            if (_typeMap.ContainsKey(type))
            {
                return _typeMap[type];
            }
            else
            {
                SqlDbType tmp;
                if (Enum.TryParse(type.ToString(), out tmp))
                    return tmp;
            }

            throw new ArgumentException("{giveType.FullName} is not a supported .NET class");
        }

        public static bool IsItAppropriateForSingleColumn(Type type)
        {
            return _appropriateTypesForSingleColumnResult.ContainsKey(type);
        }
    }
}