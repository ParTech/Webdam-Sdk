using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Webdam.Sdk.Api.Requests;
using Webdam.Sdk.Api.RequestSender;
using Webdam.Sdk.Models;

namespace Webdam.Sdk.Service.Folders
{
	internal class FolderService : IFolderService
	{
		/// <summary>
		/// Request sender to communicate with the Bynder API
		/// </summary>
		private IApiRequestSender _requestSender;


		/// <summary>
		/// Initializes a new instance of the class
		/// </summary>
		/// <param name="requestSender">instance to communicate with the Bynder API</param>
		public FolderService(IApiRequestSender requestSender)
		{
			_requestSender = requestSender;
		}

		public async Task<IList<Folder>> GetFoldersAsync()
		{
			return await _requestSender.SendRequestAsync(new ApiRequest<IList<Folder>>
			{
				Path = "/folders",
				HTTPMethod = HttpMethod.Get,
			}).ConfigureAwait(false);
		}

		public async Task<Folder> GetFolderAsync(int id)
		{
			return await _requestSender.SendRequestAsync(new ApiRequest<Folder>
			{
				Path = $"/folders/{id}",
				HTTPMethod = HttpMethod.Get,
			}).ConfigureAwait(false);
		}
	}
}
