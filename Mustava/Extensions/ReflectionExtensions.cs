using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mustava.Helpers;

namespace Mustava.Extensions
{
    public static class ReflectionExtensions
    {
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
                    propertyInfo.SetValue(obj, TypeHelpers.GetDefault(propertyInfo.PropertyType));
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
                    propertyInfo.SetValue(obj, TypeHelpers.GetDefault(propertyInfo.PropertyType));
                }
            }
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