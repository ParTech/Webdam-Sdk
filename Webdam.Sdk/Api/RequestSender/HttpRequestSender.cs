﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webdam.Sdk.Api.RequestSender
{
    /// <summary>
    /// HTTP request sender. Used to send HTTP Requests.
    /// It eases unit testing of other components checking that correct requests are being sent.
    /// </summary>
    internal class HttpRequestSender : IHttpRequestSender
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// User-Agent header we add to each request.
        /// </summary>
        public string UserAgent
        {
            get { return $"webdam-c-sharp-sdk/{Assembly.GetExecutingAssembly().GetName().Version}"; }
        }

        /// <summary>
        /// Sends the HTTP request and returns its response.
        /// </summary>
        /// <param name="httpRequest">HTTP request.</param>
        /// <returns>The HTTP request response.</returns>
        /// <exception cref="T:System.Net.Http.HttpRequestException">Check <see cref="t:Sdk.Api.IHttpRequestSender"/>.</exception>
        public async Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage httpRequest)
        {
            httpRequest.Headers.Add("User-Agent", UserAgent);
            var response = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="T:Sdk.Api.ApiRequestSender"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:Sdk.Api.ApiRequestSender"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:Sdk.Api.ApiRequestSender"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:Sdk.Api.ApiRequestSender"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:Sdk.Api.ApiRequestSender"/> was occupying.</remarks>
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
