using System;
using System.Linq;
using System.Threading.Tasks;
using Webdam.Sample.Utils;
using Webdam.Sdk.Service;
using Webdam.Sdk.Settings;

namespace Webdam.Sample
{
	public class ApiSample
	{
		private IWebdamClient _webdamClient;

		public static async Task Main(string[] args)
		{
			var configuration = PasswordConfiguration.FromJson("Config.json");
			var apiSample = new ApiSample(configuration);
			await apiSample.AuthenticateWithOAuth2Async(
				useClientCredentials: configuration.RedirectUri == null
			);
			var id = await apiSample.ListFoldersAsync();
			if (id != null)
				await apiSample.GetMediaFromFolderAsync((int)id);
		}

		private ApiSample(PasswordConfiguration configuration)
		{
			_webdamClient = ClientFactory.Create(configuration);
		}
		private async Task AuthenticateWithOAuth2Async(bool useClientCredentials)
		{
			if (useClientCredentials)
			{
				await _webdamClient.GetOAuthService().GetAccessTokenAsync();
			}
			else
			{
				Browser.Launch(_webdamClient.GetOAuthService().GetAuthorisationUrl("state example"));
				Console.WriteLine("Insert the code: ");
				var code = Console.ReadLine();
				await _webdamClient.GetOAuthService().GetAccessTokenAsync(code);
			}
		}

		private async Task<int?> ListFoldersAsync()
		{
			var folders = await _webdamClient.GetFolderService().GetFoldersAsync();
			Console.WriteLine($"Folders: [{string.Join(", ", folders.Select(x => x.Name))}]");
			return folders.FirstOrDefault(x => x.NumberOfAssets > 0)?.ID;
		}

		private async Task GetMediaFromFolderAsync(int folderId)
		{
			var folder = await _webdamClient.GetAssetService().GetMediaInFolderAsync(folderId);
			Console.WriteLine($"MediaItems: [{string.Join(", ", folder.Items.Select(x => x.Name))}]");
		}
	}
}
