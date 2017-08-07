using System;
using System.ComponentModel;
using System.Reflection;
using Mustava.Attributes;

namespace Mustava.Extensions
{
    public static class AttributeExtensions
    {
        public static string EnumGetDescription(this object obj)
        {
            if (obj == null)
            {
                return "";
            }

            var attribute = obj.GetMyAttribute<DescriptionAttribute>(obj.ToString());
            if (attribute == null)
            {
                return "";
            }

            return attribute.Description;
        }

        public static string EnumGetSymbol(this object obj)
        {
            if (obj == null)
            {
                return "";
            }

            var symbolAttribute = obj.GetMyAttribute<SymbolAttribute>(obj.ToString());
            if (symbolAttribute == null)
            {
                return "";
            }

            return symbolAttribute.Symbol;
        }

        public static T GetMyAttribute<T>(this object obj, string propertyName) where T : Attribute
        {
            if (obj == null || propertyName.IsNullOrEmpty())
            {
                return default(T);
            }

            var memberInfos = obj.GetType().GetMember(propertyName);
            if (memberInfos.Length <= 0)
            {
                return default(T);
            }

            return memberInfos[0].GetMyAttribute<T>();
        }

        public static T GetMyAttribute<T>(this PropertyInfo pi) where T : Attribute
        {
            var attributes = pi.GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                return attributes[0] is T ? (T) attributes[0] : default(T);
            }

            return default(T);
        }
        
        public static T GetMyAttribute<T>(this MemberInfo mi) where T : Attribute
        {
            var attributes = mi.GetCustomAttributes(typeof(T), false);
            if (attributes.Length > 0)
            {
                return attributes[0] is T ? (T) attributes[0] : default(T);
            }

            return default(T);
        }
    }
}