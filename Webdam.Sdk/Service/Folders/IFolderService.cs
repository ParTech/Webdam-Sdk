using System.Collections.Generic;
using System.Threading.Tasks;
using Webdam.Sdk.Models;

namespace Webdam.Sdk.Service.Folders
{
	public interface IFolderService
	{
		Task<IList<Folder>> GetFoldersAsync();

		Task<Folder> GetFolderAsync(int id);
	}
}
