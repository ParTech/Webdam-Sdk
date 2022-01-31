using System;
using Webdam.Sdk.Api.RequestSender;
using Webdam.Sdk.Models;
using Webdam.Sdk.Service.Asset;
using Webdam.Sdk.Service.Folders;
using Webdam.Sdk.Service.OAuth;
using Webdam.Sdk.Settings;

namespace Webdam.Sdk.Service
{
	public class WebdamClient : IWebdamClient
	{
		private readonly PasswordConfiguration _configuration;
		private readonly IApiRequestSender _requestSender;
		private readonly ICredentials _credentials;
		private readonly IFolderService _folderService;
		private readonly IAssetService _assetService;
		private readonly IOAuthService _oauthService;

		public WebdamClient(PasswordConfiguration configuration)
		{
			_configuration = configuration;
			_credentials = new Credentials();
			_requestSender = ApiRequestSender.Create(_configuration, _credentials, this);
			_folderService = new FolderService(_requestSender);
			_assetService = new AssetService(_requestSender);
			_oauthService = new OAuthService(_configuration, _credentials, _requestSender);
		}

		/// <summary>
		/// Check <see cref="t:Sdk.Service.IClient"/>
		/// </summary>
		public event EventHandler<Token> OnCredentialsChanged
		{
			add
			{
				_credentials.OnCredentialsChanged += value;
			}

			remove
			{
				_credentials.OnCredentialsChanged -= value;
			}
		}

		public void Dispose()
		{
			_requestSender.Dispose();
		}

		public IFolderService GetFolderService()
		{
			return _folderService;
		}

		public IAssetService GetAssetService()
		{
			return _assetService;
		}

		public IOAuthService GetOAuthService()
		{
			return _oauthService;
		}
	}
}
