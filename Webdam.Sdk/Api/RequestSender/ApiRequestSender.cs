using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Webdam.Sdk.Api.Requests;
using Webdam.Sdk.Extensions;
using Webdam.Sdk.Query.Decoder;
using Webdam.Sdk.Service;
using Webdam.Sdk.Settings;
using Webdam.Sdk.Utils;

namespace Webdam.Sdk.Api.RequestSender
{
    class ApiRequestSender : IApiRequestSender
	{
		private readonly PasswordConfiguration _configuration;
        private readonly QueryDecoder _queryDecoder = new QueryDecoder();
        private readonly ICredentials _credentials;
		private readonly IWebdamClient _webdamClient;
		private IHttpRequestSender _httpSender;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        internal ApiRequestSender(PasswordConfiguration configuration, ICredentials credentials, IWebdamClient webdamClient, IHttpRequestSender httpSender)
		{
			_configuration = configuration;
			_credentials = credentials;
			_webdamClient = webdamClient;
			_httpSender = httpSender;
		}

		/// <summary>
		/// Create an instance of <see cref="IApiRequestSender"/> given the specified configuration and credentials.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="configuration">Configuration.</param>
		/// <param name="credentials">Credentials.</param>
		/// <param name="oauthService">OAuthService.</param>
		public static IApiRequestSender Create(PasswordConfiguration configuration, ICredentials credentials, IWebdamClient webdamClient)
		{
			return new ApiRequestSender(configuration, credentials, webdamClient, new HttpRequestSender());
		}

		public void Dispose()
		{
			_httpSender.Dispose();
		}

		public async Task<T> SendRequestAsync<T>(Request<T> request)
		{
			var response = await CreateHttpRequestAsync(request).ConfigureAwait(false);

			var responseContent = response.Content;
			if (response.Content == null)
			{
				return default;
			}

			var responseString = await responseContent.ReadAsStringAsync().ConfigureAwait(false);
			if (responseString == null)
			{
				return default;
			}

			return JsonConvert.DeserializeObject<T>(responseString);
		}

        private async Task<HttpResponseMessage> CreateHttpRequestAsync<T>(Request<T> request)
        {
            var httpRequestMessage = HttpRequestMessageFactory.Create(
                _configuration.BaseUrl.ToString(),
                request.HTTPMethod,
                _queryDecoder.GetParameters(request.Query),
                request.Path
            );

            if (request.Authenticated)
            {
                if (!_credentials.AreValid())
                {
                    // Get a refesh token when the credentials are no longer valid
                    await _semaphore.WaitAsync().ConfigureAwait(false);
                    try
                    {
                        await _webdamClient.GetOAuthService().GetRefreshTokenAsync().ConfigureAwait(false);
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
                TextInfo textInfo = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                    textInfo.ToTitleCase(_credentials.TokenType),
                    _credentials.AccessToken
                );
            }

            return await _httpSender.SendHttpRequest(httpRequestMessage).ConfigureAwait(false);
        }

        private static class HttpRequestMessageFactory
        {
            internal static HttpRequestMessage Create(
                string baseUrl, HttpMethod method, IDictionary<string, string> requestParams, string urlPath)
            {
                var builder = new UriBuilder(baseUrl).AppendPath(urlPath);

                if (HttpMethod.Get == method || HttpMethod.Delete == method)
                {
                    builder.Query = Url.ConvertToQuery(requestParams);
                }

                HttpRequestMessage requestMessage = new HttpRequestMessage(method, builder.ToString());
                if (HttpMethod.Post == method)
                {
                    requestMessage.Content = new FormUrlEncodedContent(requestParams);
                }

                return requestMessage;
            }
        }
    }
}
