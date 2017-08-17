using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Mustava.Extensions
{
    public static class EnumExtensions
    {
        public static List<string> GetDescriptionAttributes<T>(this T enumType) 
            where T : struct, IConvertible
        {
            return enumType.GetAttributePropertyList<DescriptionAttribute, T>("Description")
                .Select(i => i.ExToStringOrEmpty())
                .Where(i => i != string.Empty)
                .ToList()
                ;
        }

        public static List<object> GetAttributePropertyList<TAttribute, TEnum>(this TEnum enumType, string attributePropertyName)
            where TAttribute : Attribute
            where TEnum : struct, IConvertible
        {
            if (attributePropertyName.ExIsNullOrEmpty())
            {
                return null;
            }

            return typeof(TEnum).GetFields()
                .Select(i => i.GetCustomAttributes(typeof(TAttribute), false))
                .Where(i => i != null && i.Length > 0)
                .Select(i => i[0])
                .Cast<TAttribute>()
                .Select(i => i.GetValueOfProperty(attributePropertyName))
                .Where(i => i != null)
                .ToList();
        }
    }
}