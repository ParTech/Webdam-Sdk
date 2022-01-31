using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Webdam.Sdk.Models
{
	public class Thumbnail
	{
		[JsonProperty("size")]
		public string Size { get; set; }
		[JsonProperty("url")]
		public string Url { get; set; }
	}
}
