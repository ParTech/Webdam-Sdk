using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Webdam.Sdk.Models
{
    /// <summary>
    /// Model for the response of the API.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Message from API
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        [JsonProperty("statuscode")]
        public int StatusCode { get; set; }
    }
}
