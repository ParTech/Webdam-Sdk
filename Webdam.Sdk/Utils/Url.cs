using System;
using System.Collections.Generic;
using System.Linq;

namespace Webdam.Sdk.Utils
{
	/// <summary>
	/// URL Utilities Class.
	/// </summary>
	public static class Url
    {
        /// <summary>
        /// Converts dictionary to query parameters.
        /// </summary>
        /// <returns>Escaped query.</returns>
        /// <param name="parameters">dictionary with parameters.</param>
        public static string ConvertToQuery(IDictionary<string, string> parameters)
        {
            var encodedValues = parameters.Keys
                .OrderBy(key => key)
                .Select(key => $"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(parameters[key])}");
            var queryUri = string.Join("&", encodedValues);

            return queryUri;
        }
    }
}
