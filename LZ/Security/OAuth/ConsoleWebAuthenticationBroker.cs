using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
			var requestUriStrb = new StringBuilder();
			requestUriStrb.Append(requestUri.AbsoluteUri);
			
			if (callbackUri != null)
			{
				requestUriStrb.AppendFormat("&oauth_callback={0}", callbackUri.AbsoluteUri);
			}
			
			var listener = new HttpListener();
			listener.Prefixes.Add(callbackUri.AbsoluteUri);
			listener.Start();
			
			System.Diagnostics.Process.Start(requestUriStrb.ToString());
			
			var ctx = await listener.GetContextAsync();
			
			var token = new Credential(ctx.Request.QueryString.Get("oauth_token"), ctx.Request.QueryString.Get("oauth_verifier"));
			
			// empty response
			// ctx.Response.ContentLength64 = 0;
			// await ctx.Response.OutputStream.WriteAsync(new byte[0], 0, 0);
			ctx.Response.OutputStream.Close();
			
			listener.Stop();
			
			return token;
		}

		public Uri GetCurrentApplicationCallbackUri()
		{
			return new Uri("http://localhost:8080/");
		}
	}
}