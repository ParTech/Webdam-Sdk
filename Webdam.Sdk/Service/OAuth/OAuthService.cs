using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Webdam.Sdk.Api.Requests;
using Webdam.Sdk.Api.RequestSender;
using Webdam.Sdk.Exceptions;
using Webdam.Sdk.Extensions;
using Webdam.Sdk.Models;
using Webdam.Sdk.Query;
using Webdam.Sdk.Settings;
using Webdam.Sdk.Utils;

namespace Webdam.Sdk.Service.OAuth
{
	internal class OAuthService : IOAuthService
	{
		private readonly PasswordConfiguration _configuration;
		private readonly ICredentials _credentials;
		/// <summary>
		/// Request sender to communicate with the Webdam API
		/// </summary>
		private readonly IApiRequestSender _requestSender;

		public const string AuthPath = "/oauth2/authorize";
		public const string TokenPath = "/oauth2/token";

		/// <summary>
		/// Initializes a new instance of the class
		/// </summary>
		/// <param name="requestSender">instance to communicate with the Bynder API</param>
		public OAuthService(PasswordConfiguration configuration, ICredentials credentials, IApiRequestSender requestSender)
		{
			_configuration = configuration;
			_credentials = credentials;
			_requestSender = requestSender;
		}
		public async Task GetAccessTokenAsync()
		{
			_credentials.Update(await _requestSender.SendRequestAsync(
				new OAuthRequest<Token>
				{
					Path = TokenPath,
					HTTPMethod = HttpMethod.Post,
					Query = new PasswordTokenQuery
					{
						ClientId = _configuration.ClientId,
						ClientSecret = _configuration.ClientSecret,
						Username = _configuration.Username,
						Password = _configuration.Password,
						GrantType = "password",
					},
				}
			).ConfigureAwait(false));
		}

		public async Task GetAccessTokenAsync(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				throw new ArgumentNullException(code);
			}

			_credentials.Update(await _requestSender.SendRequestAsync(
				new OAuthRequest<Token>
				{
					Path = TokenPath,
					HTTPMethod = HttpMethod.Post,
					Query = new PasswordTokenQuery
					{
						ClientId = _configuration.ClientId,
						ClientSecret = _configuration.ClientSecret,
						RedirectUri = _configuration.RedirectUri,
						Username = _configuration.Username,
						Password = _configuration.Password,
						GrantType = "authorization_code",
						Code = code,
						Scopes = _configuration.Scopes,
					},
				}
			).ConfigureAwait(false));
		}

		public string GetAuthorisationUrl(string state)
		{
			if (string.IsNullOrEmpty(state))
			{
				throw new ArgumentNullException(state);
			}

			return new UriBuilder(_configuration.BaseUrl)
			{
				Query = Url.ConvertToQuery(
					new Dictionary<string, string>
					{
						{ "client_id", _configuration.ClientId },
						{ "redirect_uri", _configuration.RedirectUri },
						{ "scope", _configuration.Scopes },
						{ "response_type", "code" },
						{ "state", state },
					}
				)
			}.AppendPath(AuthPath).ToString();
		}

		public async Task GetRefreshTokenAsync()
		{
			if (_configuration.RedirectUri == null)
			{
				await GetAccessTokenAsync();
			}
			else
			{
				if (_credentials.RefreshToken == null)
				{
					throw new MissingTokenException(
						"Access token expired and refresh token is missing. " +
						"Either pass a not expited access token through " +
						"configuration or login through OAuth2"
					);
				}
				_credentials.Update(await _requestSender.SendRequestAsync(
					new OAuthRequest<Token>
					{
						Path = TokenPath,
						HTTPMethod = HttpMethod.Post,
						Query = new PasswordTokenQuery
						{
							ClientId = _configuration.ClientId,
							ClientSecret = _configuration.ClientSecret,
							Username = _configuration.Username,
							Password = _configuration.Password,
							RefreshToken = _credentials.RefreshToken,
							GrantType = "refresh_token",
						},
					}
				).ConfigureAwait(false));
			}
		}
	}
}
