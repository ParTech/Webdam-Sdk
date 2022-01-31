using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Webdam.Sdk.Models
{
	public class Folder
	{
		[JsonProperty("id")]
		public int ID { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("numassets")]
		public int NumberOfAssets { get; set; }

		[JsonProperty("numchildren")]
		public int NumberOfChildren { get; set; }

		[JsonProperty("folders")]
		public IList<Folder> Folders { get; set; }

		[JsonProperty("items")]
		public IList<Media> Items { get; set; }
	}
}
