using System;
using System.Collections.Generic;

namespace Mustava.Extensions
{
    public static class ExceptionExtensions
    {
        public static List<Exception> GetAllInnerExceptions(this Exception exception)
        {
            if (exception == null || exception.InnerException == null)
                return new List<Exception>();

            var list = new List<Exception>();

            while (exception.InnerException != null)
            {
                list.Add(exception.InnerException);
                exception = exception.InnerException;
            }

            list.Add(exception);

            return list;
        }
    }
}