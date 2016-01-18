using System;
using System.Threading.Tasks;

namespace LZ.Security.OAuth
{
	public interface IWebAuthenticationBroker
	{
		Task<ICredential> AuthenticateAsync(Uri requestUri, Uri callbackUri = null);
		Uri GetCurrentApplicationCallbackUri();
	}
}