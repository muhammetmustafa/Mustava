﻿using System;
using Mustava.Extensions;

namespace Mustava.Helpers
{
    public static class GeneralUtils
    {
        public static bool FilterCheck<T>(T item, string propertyName, string text)
        {
            if (item == null)
                return false;

            var property = item.GetValueOfProperty(propertyName, true);

            return property != null && property.ToString().StartsWith(text, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsValidUrl(string url)
        {
            Uri uriResult;
            var result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }
    }
}