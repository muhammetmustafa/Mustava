using System;
using Mustava.Extensions;

namespace Mustava.Helpers
{
    public static class UrlUtils
    {
        public static string GetResourcePart(string url)
        {
            if (url.ExIsNullOrEmpty())
            {
                return null;
            }
            
            var lastindex = url.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);
            if (lastindex < 0)
            {
                return null;
            }

            lastindex++;

            return url.Substring(lastindex, url.Length - lastindex);
        }
    }
}