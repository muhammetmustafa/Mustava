using System;
using System.Collections.Generic;
using System.Data;

namespace Mustava.Ado
{
    public class AdoTypeMap
    {
        private static readonly Dictionary<Type, SqlDbType> _typeMap;

        static AdoTypeMap()
        {
            _typeMap = new Dictionary<Type, SqlDbType>();

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
    }
}