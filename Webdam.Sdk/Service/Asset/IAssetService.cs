using System.Collections.Generic;
using System.Threading.Tasks;
using Webdam.Sdk.Models;

namespace Webdam.Sdk.Service.Asset
{
	public interface IAssetService
	{ 
		Task<Folder> GetMediaInFolderAsync(int id);

		Task<Media> GetMediaItemAsync(int id);

		Task<EmbedLinks> GetEmbedLinksAsync(int id);
	}
}
