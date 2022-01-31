using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Webdam.Sdk.Models
{
	public class EmbedLinks
	{
		[JsonProperty("pagelinks")]
		public Dictionary<string,string> PageLinks { get; set; }
		[JsonProperty("directlinks")]
		public Dictionary<string,string> DirectLinks { get; set; }
		[JsonProperty("embedlink")]
		public Dictionary<string,string> EmbedLink { get; set; }
		[JsonProperty("dat")]
		public string TransformURL { get; set; }
		[JsonProperty("directdownloadlink")]
		public string DirectDownloadLink { get; set; }
	}
}
