using System;
using System.IO;
using Newtonsoft.Json;

namespace Webdam.Sdk.Settings
{
	public class PasswordConfiguration
	{
        /// <summary>
        /// Webdam domain Url we want to communicate with
        /// </summary>
        [JsonProperty("base_url")]
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// OAuth Client id
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// OAuth Client secret
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI.
        /// Optional: It is only needed if trying to login with OAuth using
        /// an authorization code.
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// List of scopes to request to be granted to the access token. Can
        /// only be a subset of the scopes requested in the Authorize
        /// application request.
        /// Optional: When not passed, all the scopes will be requested.
        /// </summary>
        [JsonProperty("scopes")]
        public string Scopes { get; set; }

        /// <summary>
        /// OAuth Username
        /// </summary>
        [JsonProperty("username")]
		public string Username { get; set; }

        /// <summary>
        /// OAuth Password
        /// </summary>
		[JsonProperty("password")]
		public string Password { get; set; }

        /// <summary>
        /// Create a <see cref="PasswordConfiguration"/> using the given filepath
        /// </summary>
        /// <param name="filepath">JSON file path</param>
        /// <returns><see cref="PasswordConfiguration"/> instance</returns>
        public static PasswordConfiguration FromJson(string filepath)
		{
			return JsonConvert.DeserializeObject<PasswordConfiguration>(File.ReadAllText(filepath));
		}
	}
}
