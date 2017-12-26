using System;
using System.Collections.Generic;
using Mustava.Extensions;

namespace Mustava.Helpers
{
    public static class CollectionHelpers
    {
        public static List<T> InitializeListWithClones<T>(T obj, int count)
        {
            var list = new List<T>();

            list.Add(obj);

            for (var i = 1; i < count; i++)
            {
                var clone = Activator.CreateInstance<T>();
                clone.SetAllMyMembers(obj);

                list.Add(clone);
            }

            return list;
        } 
    }
}