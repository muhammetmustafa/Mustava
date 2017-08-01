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

            var memberInfo = obj.GetType().GetMember(obj.ToString());
            if (memberInfo.Length <= 0)
            {
                return "";
            }

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length <= 0)
            {
                return "";
            }

            var attribute = attributes[0] as DescriptionAttribute;
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

            var memberInfo = obj.GetType().GetMember(obj.ToString());
            if (memberInfo.Length <= 0)
            {
                return "";
            }

            var attributes = memberInfo[0].GetCustomAttributes(typeof (SymbolAttribute), false);
            if (attributes.Length <= 0)
            {
                return "";
            }

            var symbolAttribute = attributes[0] as SymbolAttribute;
            if (symbolAttribute == null)
            {
                return "";
            }

            return symbolAttribute.Symbol;
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