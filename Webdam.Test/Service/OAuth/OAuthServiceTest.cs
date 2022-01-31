using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Webdam.Sdk.Api.Requests;
using Webdam.Sdk.Api.RequestSender;
using Webdam.Sdk.Models;
using Webdam.Sdk.Query;
using Webdam.Sdk.Service.OAuth;
using Webdam.Sdk.Settings;
using Xunit;

namespace Webdam.Test.Service.OAuth
{
	public class OAuthServiceTest
	{
        private const string _clientId = "clientId";
        private const string _clientSecret = "clientSecret";
        private const string _baseUrl = "https://example.bynder.com";
        private const string _redirectUrl = "https://redirect.bynder.com";
        private const string _redirectUrlEncoded = "https%3A%2F%2Fredirect.bynder.com";
        private const string _refreshToken = "refreshToken";
        private const string _state = "state";
        private const string _scope = "offline asset:read";
        private const string _scopeEncoded = "offline%20asset%3Aread";
        private const string _code = "code";

        private Token _token;
        private Mock<ICredentials> _credentialsMock;
        private Mock<IApiRequestSender> _apiRequestSenderMock;
        private OAuthService _oauthServiceClientCreds;
        private OAuthService _oauthServiceAuthCode;

        public OAuthServiceTest()
        {
            _token = new Token();

            _credentialsMock = new Mock<ICredentials>();
            _credentialsMock
                .SetupGet(credentials => credentials.RefreshToken)
                .Returns(_refreshToken);

            _apiRequestSenderMock = new Mock<IApiRequestSender>();
            _apiRequestSenderMock
                .Setup(sender => sender.SendRequestAsync(It.IsAny<OAuthRequest<Token>>()))
                .Returns(Task.FromResult(_token));

            _oauthServiceClientCreds = new OAuthService(
                new PasswordConfiguration
                {
                    BaseUrl = new Uri(_baseUrl),
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                    Scopes = _scope,
                },
                _credentialsMock.Object,
                _apiRequestSenderMock.Object
            );

            _oauthServiceAuthCode = new OAuthService(
                new PasswordConfiguration
                {
                    BaseUrl = new Uri(_baseUrl),
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                    RedirectUri = _redirectUrl,
                    Scopes = _scope,
                },
                _credentialsMock.Object,
                _apiRequestSenderMock.Object
            );
        }

        [Fact]
        public void GetAuthorisationUrlReturnsCorrectUrl()
        {
            var expectedAuthorisationUrl = $"{_baseUrl}:443{OAuthService.AuthPath}" +
                $"?client_id={_clientId}" +
                $"&redirect_uri={_redirectUrlEncoded}" +
                $"&response_type={_code}" +
                $"&scope={_scopeEncoded}" +
                $"&state={_state}";
            var authorisationUrl = _oauthServiceAuthCode.GetAuthorisationUrl(_state);

            Assert.Equal(expectedAuthorisationUrl, authorisationUrl);
        }

        private void CheckTokenIsUpdated()
        {
            _credentialsMock.Verify(cred => cred.Update(
                It.Is<Token>(
                    token => token == _token
                )
            ));
        }

        private void CheckClientCredsRequest()
        {
            _apiRequestSenderMock.Verify(sender => sender.SendRequestAsync(
                It.Is<OAuthRequest<Token>>(req =>
                    req.Path == OAuthService.TokenPath
                    && req.HTTPMethod == HttpMethod.Post
                    && (req.Query as PasswordTokenQuery).ClientId == _clientId
                    && (req.Query as PasswordTokenQuery).ClientSecret == _clientSecret
                    && (req.Query as PasswordTokenQuery).GrantType == "client_credentials"
                    && (req.Query as PasswordTokenQuery).Scopes == _scope
                )
             ));
        }

        [Fact]
        public async Task GetAccessTokenWithClientCreds()
        {
            await _oauthServiceClientCreds.GetAccessTokenAsync().ConfigureAwait(false);

            CheckClientCredsRequest();
            CheckTokenIsUpdated();
        }

        [Fact]
        public async Task GetRefreshTokenWithClientCreds()
        {
            await _oauthServiceClientCreds.GetRefreshTokenAsync().ConfigureAwait(false);

            CheckClientCredsRequest();
            CheckTokenIsUpdated();
        }

        [Fact]
        public async Task GetAccessTokenWithAuthCode()
        {
            await _oauthServiceAuthCode.GetAccessTokenAsync(_code).ConfigureAwait(false);

            _apiRequestSenderMock.Verify(sender => sender.SendRequestAsync(
                It.Is<OAuthRequest<Token>>(req =>
                    req.Path == OAuthService.TokenPath
                    && req.HTTPMethod == HttpMethod.Post
                    && (req.Query as PasswordTokenQuery).ClientId == _clientId
                    && (req.Query as PasswordTokenQuery).ClientSecret == _clientSecret
                    && (req.Query as PasswordTokenQuery).RedirectUri == _redirectUrl
                    && (req.Query as PasswordTokenQuery).GrantType == "authorization_code"
                    && (req.Query as PasswordTokenQuery).Code == _code
                    && (req.Query as PasswordTokenQuery).Scopes == _scope
                )
             ));

            CheckTokenIsUpdated();
        }

        [Fact]
        public async Task GetRefreshTokenWithAuthCode()
        {
            await _oauthServiceAuthCode.GetRefreshTokenAsync().ConfigureAwait(false);

            _apiRequestSenderMock.Verify(sender => sender.SendRequestAsync(
                It.Is<OAuthRequest<Token>>(req =>
                    req.Path == OAuthService.TokenPath
                    && req.HTTPMethod == HttpMethod.Post
                    && (req.Query as PasswordTokenQuery).ClientId == _clientId
                    && (req.Query as PasswordTokenQuery).ClientSecret == _clientSecret
                    && (req.Query as PasswordTokenQuery).RefreshToken == _refreshToken
                    && (req.Query as PasswordTokenQuery).GrantType == "refresh_token"
                )
            ));

            CheckTokenIsUpdated();
        }

    }
}
