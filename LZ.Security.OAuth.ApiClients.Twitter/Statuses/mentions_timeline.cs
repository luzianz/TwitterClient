using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LZ.Security.OAuth.ApiClients.Twitter.Statuses
{
	public static partial class StatusesExtensions
	{
		/// <summary>
		/// Returns the 20 most recent mentions (tweets containing a users’s @screen_name) for the authenticating user.
		/// The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.
		/// This method can only return up to 800 tweets.
		/// </summary>
		/// <param name="count">Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method</param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available</param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to the specified ID</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object</param>
		/// <param name="contributor_details">This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included</param>
		/// <param name="include_entities">The entities node will be disincluded when set to false</param>
		public static Task<HttpResponseMessage> GetMentionsTimeline(
			this HttpClient client,
			ICredential consumerCredential,
			ICredential accessToken,
			int? count = null,
			int? since_id = null,
			int? max_id = null,
			bool trim_user = false,
			bool contributor_details = false,
			bool include_entities = true)
		{
			var parameters = new Dictionary<string, string>();

			#region Add Parameters
			if (count.HasValue) parameters.Add(nameof(count), count.Value.ToString());
			if (since_id.HasValue) parameters.Add(nameof(since_id), since_id.Value.ToString());
			if (max_id.HasValue) parameters.Add(nameof(max_id), max_id.Value.ToString());
			if (trim_user) parameters.Add(nameof(trim_user), trim_user.ToString());
			if (contributor_details) parameters.Add(nameof(contributor_details), contributor_details.ToString());
			if (!include_entities) parameters.Add(nameof(include_entities), include_entities.ToString());
			#endregion

			var uri = BuildUri("mentions_timeline.json", parameters);
			return client.AccessResourceAsync(
				uri,
				HttpMethod.Get,
				consumerCredential,
				accessToken,
				parameters);
		}
	}
}