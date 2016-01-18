using System;
using System.Net.Http;
using System.Threading.Tasks;
using LZ.Security;
using LZ.Security.OAuth;
using LZ.Security.OAuth.ApiClients.Twitter;
using LZ.Security.OAuth.ApiClients.Twitter.Statuses;
using Microsoft.Extensions.Configuration;

namespace TwitterClientDemo
{
    static class Program
	{
		private static ICredential consumerCredentials;
		private static ICredential accessToken;
		
		public static IConfiguration Configuration { get; set; }
		
		static void Main()
		{
			var builder = new ConfigurationBuilder();

			// You'll need to create your own secrets.json. See README.md
			builder.AddJsonFile("secrets.json");
			Configuration = builder.Build();

			if (!Configuration.TryGetCredential(nameof(consumerCredentials), out consumerCredentials))
			{
				throw new InvalidOperationException("consumerCredentials required in secrets.json");
			}
			
			Configuration.TryGetCredential(nameof(accessToken), out accessToken);
			
			Run().Wait();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}
		
		static async Task Run()
		{			
			if (accessToken == null)
			{
				var authorizer = new Authorizer(consumerCredentials, new ConsoleWebAuthenticationBroker());
				accessToken = await authorizer.AuthorizeAsync(new TwitterUriSet());
			}
			
			var client = new HttpClient();
			var response = await client.GetUserTimelineAsync(consumerCredentials, accessToken, screen_name: "luzianz", count: 10, trim_user: true, exclude_replies: true);
			Console.WriteLine(await response.Content.ReadAsStringAsync());
		}
	}
}