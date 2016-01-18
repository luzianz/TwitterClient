using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LZ.Security.OAuth
{
	public class Authorizer
	{
		#region Fields

		private readonly ICredential consumerCredentials;
		private readonly IWebAuthenticationBroker webAuthenticationBroker;
		
		#endregion

		#region Constructor

		public Authorizer(ICredential consumerCredentials, IWebAuthenticationBroker webAuthenticationBroker)
		{
			this.consumerCredentials = consumerCredentials;
			this.webAuthenticationBroker = webAuthenticationBroker;
		}

		#endregion
		
		/// <returns>Access Token</returns>
		public async Task<ICredential> AuthorizeAsync(IAuthUriSet authUriSet)
		{
			var client = new HttpClient();
			Uri callback = webAuthenticationBroker.GetCurrentApplicationCallbackUri();

			var requestToken = await client.ObtainRequestTokenAsync(
				requestUri: authUriSet.RequestTokenUri,
				callbackUri: callback,
				httpMethod: HttpMethod.Post,
				consumerCredentials: consumerCredentials);

			string fullAuthorizeUrlStr = string.Format("{0}?oauth_token={1}", authUriSet.AuthorizeUri.AbsoluteUri, requestToken.Key);

			var authorizationToken = await webAuthenticationBroker.AuthenticateAsync(
				new Uri(fullAuthorizeUrlStr),
				callback);

			var accessToken = await client.ObtainAccessTokenAsync(
				requestUri: authUriSet.AccessTokenUri,
				httpMethod: HttpMethod.Post,
				consumerCredentials: consumerCredentials,
				authorizationToken: authorizationToken,
				requestToken: requestToken);

			return accessToken;
		}
	}
}