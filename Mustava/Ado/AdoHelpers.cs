using System;

namespace Mustava.Ado
{
    public static class AdoHelpers
    {
        public static dynamic SafeConvertType(object obj, Type type)
        {
            if (obj == DBNull.Value || obj == null)
            {
                return null;
            }

            return Convert.ChangeType(obj, type);
        }
    }
}