using LZ.Format.Web;
using System;
using System.Collections.Generic;

namespace LZ.Security.OAuth.ApiClients.Twitter.Statuses
{
	public static partial class StatusesExtensions
	{
		private const string baseUrl = "https://api.twitter.com/1.1/statuses";
		
		private static Uri BuildUri(string resource, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			return new Uri($"{baseUrl}/{resource}?{parameters.ToEncodedQueryString()}");
        }
	}
}