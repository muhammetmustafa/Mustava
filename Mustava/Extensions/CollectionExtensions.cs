using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mustava.Helper;

namespace Mustava.Extensions
{
    public static class CollectionExtensions
    {
        public static string Concatenate(this ISet<string> set, string joiner)
        {
            return set.ToList().Concatenate(joiner);
        }

        public static string Concatenate(this IEnumerable set, string joiner)
        {
            if (set == null || joiner.IsNullOrEmpty())
                return string.Empty;

            var join = new StringBuilder();
            foreach (var item in set)
            {
                join.Append(joiner);
                join.Append(item);
            }
            
            return join.ToString().Equals(string.Empty) ? string.Empty : join.ToString().Substring(joiner.Length);
        }

        public static string ConcatenateWithReflection(this ICollection set, string joiner, string propertyName)
        {
            if (set == null || set.Count <= 0 || joiner.IsNullOrEmpty())
                return string.Empty;

            var join = new StringBuilder();
            foreach (var item in set)
            {
                var propertyValue = item.GetValueOfProperty(propertyName, true);
                if (propertyValue.ToStringOrEmpty().IsEmpty())
                    continue;

                join.Append(joiner);
                join.Append(propertyValue.ToStringOrEmpty());
            }

            return join.ToString().Substring(joiner.Length);
        }

        public static T FirstElement<T>(this IList<T> list)
        {
            if (list == null || list.Count <= 0)
                return default(T);

            return list[0];
        }

        public static bool FirstElement<T>(this IList<T> list, T element)
        {
            if (list == null || list.Count <= 0)
                return false;

            return list[0].Equals(element);
        }

        public static T LastElement<T>(this IList<T> list)
        {
            if (list == null || list.Count <= 0)
                return default(T);

            return list[list.Count - 1];
        }

        public static bool LastElement<T>(this IList<T> list, T element)
        {
            if (list == null || list.Count <= 0)
                return false;

            return list[list.Count - 1].Equals(element);
        }

        public static IList<T> Clone<T>(this IList<T> list)
        {
            var clone = new List<T>();
            foreach (var item in list)
            {
                var cloneable = item as ICloneable;
                if (cloneable != null)
                    clone.Add((T)cloneable.Clone());
            }
            return clone;
        }

        public static bool AddNotNull<T>(this IList<T> list, T item)
        {
            if (item == null || list == null)
                return false;

            list.Add(item);
            return true;
        }

        public static bool AddNotNullNotDuplicate<T>(this IList<T> list, T item)
        {
            if (item == null || list == null || list.Contains(item))
                return false;

            list.Add(item);

            return true;
        }

        public static bool AddCopies<T>(this IList<T> list, T item, int times)
        {
            if (list == null || times <= 0)
                return false;

            for (int i = 0; i < times; i++)
            {
                list.Add(item);
            }

            return true;
        }

        public static bool NullOrEmpty(this IList list)
        {
            return list == null || list.Count <= 0;
        }

        public static void RecursiveTraveler<T>(this IEnumerable collection, string childrenPropertyName, Action<T> action)
        {
            if (collection == null || action == null || childrenPropertyName.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in collection)
            {
                var citem = item is T ? (T) item : default(T);
                if (citem == null)
                {
                    continue;
                }

                action.Invoke(citem);

                if (item.HasProperty(childrenPropertyName))
                {
                    var childCollection = item.GetValueOfProperty(childrenPropertyName) as IEnumerable<T>;
                    if (childCollection != null)
                    {
                        childCollection.RecursiveTraveler(childrenPropertyName, action);
                    }
                }
            }
        }

        public static void RecursiveTraveler<T>(this IEnumerable<T> collection, string childrenPropertyName, Action<T> action)
        {
            if (collection == null || action == null || childrenPropertyName.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in collection)
            {
                action.Invoke(item);

                if (item.HasProperty(childrenPropertyName))
                {
                    var childCollection = item.GetValueOfProperty(childrenPropertyName) as IEnumerable<T>;
                    if (childCollection != null)
                    {
                        childCollection.RecursiveTraveler(childrenPropertyName, action);
                    }
                }
            }
        }
        
        public static IEnumerable<List<T>> SplitList<T>(this List<T> list, int sublistSize = 30)
        {
            for (var i = 0; i < list.Count; i += sublistSize)
            {
                yield return list.GetRange(i, Math.Min(sublistSize, list.Count - i));
            }
        }
    }
}