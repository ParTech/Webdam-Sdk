using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webdam.Sdk.Api.Requests;

namespace Webdam.Sdk.Api.RequestSender
{
	internal interface IApiRequestSender : IDisposable
	{
        /// <summary>
        /// Sends the request async.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <typeparam name="T">Type we want to deserialize response to.</typeparam>
        /// <returns>The deserialized response.</returns>
        /// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue
        /// such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        Task<T> SendRequestAsync<T>(Request<T> request);
    }
}
