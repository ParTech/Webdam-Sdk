namespace Webdam.Sdk.Api.Requests
{
	internal class OAuthRequest<T> : Request<T>
	{
		internal OAuthRequest()
		{
			_authenticated = false;
		}
	}
}
