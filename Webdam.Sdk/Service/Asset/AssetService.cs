using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Webdam.Sdk.Api.Requests;
using Webdam.Sdk.Api.RequestSender;
using Webdam.Sdk.Models;

namespace Webdam.Sdk.Service.Asset
{
	internal class AssetService : IAssetService
	{
		/// <summary>
		/// Request sender to communicate with the Webdam API
		/// </summary>
		private IApiRequestSender _requestSender;


		/// <summary>
		/// Initializes a new instance of the class
		/// </summary>
		/// <param name="requestSender">instance to communicate with the Webdam API</param>
		public AssetService(IApiRequestSender requestSender)
		{
			_requestSender = requestSender;
		}

		public async Task<Folder> GetMediaInFolderAsync(int id)
		{
			return await _requestSender.SendRequestAsync(new ApiRequest<Folder>
			{
				Path = $"/folders/{id}/assets",
				HTTPMethod = HttpMethod.Get,
			}).ConfigureAwait(false);
		}

		public async Task<Media> GetMediaItemAsync(int id)
		{
			return await _requestSender.SendRequestAsync(new ApiRequest<Media>
			{
				Path = $"/assets/{id}",
				HTTPMethod = HttpMethod.Get,
			}).ConfigureAwait(false);
		}

		public async Task<EmbedLinks> GetEmbedLinksAsync(int id)
		{
			return await _requestSender.SendRequestAsync(new ApiRequest<EmbedLinks>
			{
				Path = $"/assets/{id}/embedlinks",
				HTTPMethod = HttpMethod.Get,
			}).ConfigureAwait(false);
		}
	}
}
