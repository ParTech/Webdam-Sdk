using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Webdam.Sdk.Api.Requests
{
    /// <summary>
    /// API Request representation.
    /// </summary>
    /// <typeparam name="T">Type to which the response will be deserialized</typeparam>
    internal abstract class Request<T>
    {
        protected bool _authenticated = true;

        /// <summary>
        /// Indicates whether the request needs to be authenticated.
        /// </summary>
        internal bool Authenticated { get { return _authenticated; } }

        /// <summary>
        /// HttpMethod to use.
        /// </summary>
        internal HttpMethod HTTPMethod { get; set; }

        /// <summary>
        /// Path of the endpoint to call.
        /// </summary>
        internal string Path { get; set; }

        /// <summary>
        /// Optional: Object with information about the API parameters to send.
        /// </summary>
        internal object Query { get; set; }
    }
}
