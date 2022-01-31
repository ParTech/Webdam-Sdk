using Webdam.Sdk.Models;

namespace Webdam.Sdk.Api.Requests
{
	internal class ApiRequest<T> : Request<T>
	{
	}

    /// <summary>
    /// API request where the response has an empty body, or a body with an unknown structure.
    /// </summary>
    internal class ApiRequest : Request<Status>
    {
    }
}
