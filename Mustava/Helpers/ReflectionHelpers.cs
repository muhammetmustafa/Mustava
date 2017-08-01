using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Mustava.Extensions;

namespace Mustava.Helpers
{
    public static class ReflectionHelpers
    {
        /// <summary>
        /// C# 6.0 'date nameof operatörünün işini yapar. field'ın ismini öğrenmek için kullanıyoruz.
        /// 
        /// MyClass >> int Field;
        /// 
        /// GetMemberName((MyClass myClass) => myClass.Field)
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="memberAccess"></param>
        /// <returns></returns>
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
    }
}