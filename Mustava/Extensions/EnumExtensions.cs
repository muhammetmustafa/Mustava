using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Mustava.Extensions
{
    public static class EnumExtensions
    {
        public static List<string> GetDescriptionAttributes<T>(this T enumType) where T : struct, IConvertible
        {
            return typeof(T).GetFields()
                .Select(i => i.GetCustomAttributes(typeof(DescriptionAttribute), false))
                .Where(i => i != null && i.Length > 0)
                .Select(i => i[0])
                .Cast<DescriptionAttribute>()
                .Select(i => i.Description)
                .ToList();
        }  
    }
}