using System;

namespace Mustava.Helpers
{
    public static class TypeHelpers
    {
        
        public static object GetDefault(Type type)
        {
            if (type == typeof(string))
                return string.Empty;

            if (type == typeof(int) || type == typeof(long))
                return 0;

            if (type == typeof(DateTime))
                return DateTime.MinValue;

            return null;
        }
    }
}