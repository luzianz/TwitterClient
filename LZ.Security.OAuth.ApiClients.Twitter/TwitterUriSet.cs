using System;

namespace LZ.Security.OAuth.ApiClients.Twitter
{
	public class TwitterUriSet : IAuthUriSet
	{
		#region IAuthUriSet

		public Uri AuthorizeUri
		{
			get
			{
				return new Uri("https://api.twitter.com/oauth/authorize");
			}
		}

		public Uri RequestTokenUri
		{
			get
			{
				return new Uri("https://api.twitter.com/oauth/request_token");
			}
		}

		public Uri AccessTokenUri
		{
			get
			{
				return new Uri("https://api.twitter.com/oauth/access_token");
			}
		}

		#endregion
	}
}
