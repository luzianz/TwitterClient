using Microsoft.Net.Http.Server;
using System;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.WebUtilities.QueryHelpers;

namespace LZ.Security.OAuth
{
	public class ConsoleWebAuthenticationBroker : IWebAuthenticationBroker
	{
		private readonly Encoding utf8;

		public ConsoleWebAuthenticationBroker()
		{
			utf8 = System.Text.Encoding.GetEncoding(65001);
		}
		
		public async Task<ICredential> AuthenticateAsync(Uri requestUri, Uri callbackUri = null)
		{
			var settings = new WebListenerSettings();
			var requestUriStrb = new StringBuilder();
			requestUriStrb.Append(requestUri.AbsoluteUri);

			if (callbackUri != null)
			{
				settings.UrlPrefixes.Add(callbackUri.AbsoluteUri);
				requestUriStrb.AppendFormat("&oauth_callback={0}", callbackUri.AbsoluteUri);
			}
			
			using (var listener = new WebListener(settings))
			{
				listener.Start();
				
				using (var ctx = await listener.AcceptAsync())
				{
					var query = ParseQuery(ctx.Request.QueryString);
					var oauth_token = query["oauth_token"].ToString();
					var oauth_verifier = query["oauth_verifier"].ToString();
					var token = new Credential(oauth_token, oauth_verifier);

					return token;
				}
			}
		}

		public Uri GetCurrentApplicationCallbackUri()
		{
			return new Uri("http://localhost:8080/");
		}
	}
}