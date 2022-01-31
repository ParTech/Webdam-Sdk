using System;
using System.Collections.Generic;
using System.Text;
using Webdam.Sdk.Settings;

namespace Webdam.Sdk.Service
{
	public static class ClientFactory
	{
		public static IWebdamClient Create(PasswordConfiguration configuration)
		{
			return new WebdamClient(configuration);
		}
	}
}
