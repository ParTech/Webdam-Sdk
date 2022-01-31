using System;
using Webdam.Sdk.Models;
using Webdam.Sdk.Service.Asset;
using Webdam.Sdk.Service.Folders;
using Webdam.Sdk.Service.OAuth;

namespace Webdam.Sdk.Service
{
	public interface IWebdamClient
	{
        /// <summary>
        /// Occurs when credentials changed, and that happens every time
        /// the access token is refreshed.
        /// </summary>
        event EventHandler<Token> OnCredentialsChanged;

        /// <summary>
        /// Gets the folder service to interact with assets in your Webdam portal.
        /// </summary>
        /// <returns>The asset service.</returns>
        IFolderService GetFolderService();

        /// <summary>
        /// Gets the asset service to interact with assets in your Webdam portal.
        /// </summary>
        /// <returns>The asset service.</returns>
        IAssetService GetAssetService();

        /// <summary>
        /// Gets the OAuth service.
        /// </summary>
        /// <returns>The OAuth service.</returns>
        IOAuthService GetOAuthService();
    }
}
