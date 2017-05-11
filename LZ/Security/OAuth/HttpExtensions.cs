using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LZ.Security.OAuth
{
	public static partial class HttpExtensions
	{
		public async static Task<RequestToken> ObtainRequestTokenAsync(
			this HttpClient httpClient, 
			Uri requestUri, 
			Uri callbackUri, 
			HttpMethod httpMethod, 
			ICredential consumerCredentials)
		{
			var httpRequest = new HttpRequestMessage(httpMethod, requestUri);
			var parameters = new Dictionary<string, string>();

			string oauthHeader = parameters.GenerateOAuthHeader(
				httpMethod.Method,
				consumerCredentials.Key,
				consumerCredentials.Secret,
				requestUri,
				callbackUri);

			httpRequest.Headers.Authorization = new AuthenticationHeaderValue("OAuth", oauthHeader);

			HttpResponseMessage response = await httpClient.SendAsync(httpRequest);
			string responseString = await response.Content.ReadAsStringAsync();
			
			return ParseRequestToken(responseString);
		}
		
		/// <returns>An access token</returns>
		public async static Task<ICredential> ObtainAccessTokenAsync(
			this HttpClient httpClient, 
			Uri requestUri, 
			HttpMethod httpMethod, 
			ICredential consumerCredentials, 
			ICredential authorizationToken, 
			RequestToken requestToken)
		{
			var httpRequest = new HttpRequestMessage(httpMethod, requestUri);
			var parameters = new Dictionary<string, string>();

			string oauthHeader = parameters.GenerateOAuthHeader(
				httpMethod.Method,
				consumerCredentials.Key,
				consumerCredentials.Secret,
				requestUri,
				token: authorizationToken.Key,
				tokenSecret: requestToken.Secret,
				verifier: authorizationToken.Secret);

			httpRequest.Headers.Authorization = new AuthenticationHeaderValue("OAuth", oauthHeader);

			HttpResponseMessage response = await httpClient.SendAsync(httpRequest);
			string responseString = await response.Content.ReadAsStringAsync();
			
			return ParseAccessToken(responseString);
		}

		public async static Task<HttpResponseMessage> AccessResourceAsync(
			this HttpClient httpClient,
			Uri resourceUri,
			HttpMethod httpMethod,
			ICredential consumerCredentials,
			ICredential accessToken,
			Dictionary<string, string> parameters)
		{
			var httpRequest = new HttpRequestMessage(httpMethod, resourceUri);
			string oauthHeader = parameters.GenerateOAuthHeader(
				httpMethod: httpMethod.Method,
				consumerKey: consumerCredentials.Key,
				consumerSecret: consumerCredentials.Secret,
				requestUrl: resourceUri,
				token: accessToken.Key,
				tokenSecret: accessToken.Secret);

			httpRequest.Headers.Authorization = new AuthenticationHeaderValue("OAuth", oauthHeader);

			return await httpClient.SendAsync(httpRequest);
		}
		
		/// <returns>An access token</returns>
		private static ICredential ParseAccessToken(string responseString)
		{
			AuthorizationResult result;
			if (!TokenParser.TryParseQueryString(responseString, out result))
			{
				System.Diagnostics.Debugger.Break();
				throw new Exception();
			}

			return new Credential(result.Key, result.Secret);
		}

		private static RequestToken ParseRequestToken(string responseString)
		{
			AuthorizationResult result;
			if (!TokenParser.TryParseQueryString(responseString, out result))
			{
				System.Diagnostics.Debugger.Break();
				throw new Exception();
			}

			return new RequestToken(result.Key, result.Secret)
			{
				IsCallbackConfirmed = result.IsCallbackConfirmed
			};
		}
	}
}
