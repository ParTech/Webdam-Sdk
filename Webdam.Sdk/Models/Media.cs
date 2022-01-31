using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Webdam.Sdk.Models
{
	public class Media

	{
		/// <summary>
		/// Id of the item
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Version
		/// </summary>
		[JsonProperty("version")]
		public int Version { get; set; }
		/// <summary>
		/// Name
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }
		/// <summary>
		/// File Name
		/// </summary>
		[JsonProperty("filename")]
		public string FileName { get; set; }

		/// <summary>
		/// The description of the item
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }
		/// <summary>
		/// File size of the item in Megabytes
		/// </summary>
		[JsonProperty("filesize")]
		public float Filesize { get; set; }
		/// <summary>
		/// File type extension
		/// </summary>
		[JsonProperty("filetype")]
		public string Type { get; set; }
		/// <summary>
		/// Width
		/// </summary>
		[JsonProperty("width")]
		public int Width { get; set; }

		/// <summary>
		/// Height
		/// </summary>
		[JsonProperty("height")]
		public int Height { get; set; }

		/// <summary>
		/// The raw media url for the item
		/// </summary>
		[JsonProperty("hiResURLRaw")]
		public string MediaUrl { get; set; }

		[JsonProperty("thumbnailurls")]
		public IList<Thumbnail> Thumbnails { get; set; }


		[JsonProperty("datecreated")]
		public DateTime DateCreated { get; set; }
		[JsonProperty("datemodified")]
		public DateTime DateModified { get; set; }
	}
}
