using LZ.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TwitterClientDemo
{
	public static class Global
	{
		private static Lazy<IConfigurationRoot> _ConfigurationLazy;
		private static Lazy<ICredential> _ConsumerCredentialsLazy;
		private static Lazy<ICredential> _AccessTokenLazy;

		static Global()
		{
			_ConfigurationLazy = new Lazy<IConfigurationRoot>(() => {
				var builder = new ConfigurationBuilder();

				// You'll need to create your own secrets.json. See README.md
				builder.AddJsonFile("secrets.json");
				builder.SetBasePath(Directory.GetCurrentDirectory());
				Console.WriteLine(Directory.GetCurrentDirectory());

				return builder.Build();
			});
			_ConsumerCredentialsLazy = new Lazy<ICredential>(() => {
				ICredential consumerCredentials;
				Configuration.TryGetCredential(nameof(consumerCredentials), out consumerCredentials);
				return consumerCredentials;
			});
			_AccessTokenLazy = new Lazy<ICredential>(() => {
				ICredential accessToken;
				Configuration.TryGetCredential(nameof(accessToken), out accessToken);
				return accessToken;
			});
		}

		public static IConfigurationRoot Configuration
		{
			get
			{
				return _ConfigurationLazy.Value;
			}
		}
		public static ICredential ConsumerCredentials
		{
			get
			{
				return _ConsumerCredentialsLazy.Value;
			}
		}
		public static ICredential AccessToken
		{
			get
			{
				return _AccessTokenLazy.Value;
			}
		}
	}
}