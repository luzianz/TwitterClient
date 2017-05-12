using LZ.Security;
using LZ.Security.OAuth;
using LZ.Security.OAuth.ApiClients.Twitter;
using LZ.Security.OAuth.ApiClients.Twitter.Statuses;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwitterClientDemo
{
	using static Global;

	class Program
	{
		static void Main()
		{
			Run(ConsumerCredentials, AccessToken).Wait();
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}
		
		static async Task Run(ICredential consumerCredentials, ICredential accessToken)
		{
			if (consumerCredentials == null) throw new ArgumentNullException(nameof(consumerCredentials));
			
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