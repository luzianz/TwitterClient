using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LZ.Security.OAuth.ApiClients.Twitter.Statuses
{
	public static partial class StatusesExtensions
	{
		/// <summary>
		/// Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters.
		/// User timelines belonging to protected users may only be requested when the authenticated user either “owns” the timeline or is an approved follower of the owner.
		/// The timeline returned is the equivalent of the one seen when you view a user’s profile on twitter.com.
		/// This method can only return up to 3,200 of a user’s most recent Tweets. Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.
		/// </summary>
		/// <param name="user_id">The ID of the user for whom to return results for.</param>
		/// <param name="screen_name">The screen name of the user for whom to return results for</param>
		/// <param name="since_id">Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available</param>
		/// <param name="count">Specifies the number of tweets to try and retrieve, up to a maximum of 200 per distinct request. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method</param>
		/// <param name="max_id">Returns results with an ID less than (that is, older than) or equal to the specified ID</param>
		/// <param name="trim_user">When set to either true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object</param>
		/// <param name="exclude_replies">This parameter will prevent replies from appearing in the returned timeline. Using exclude_replies with the count parameter will mean you will receive up-to count tweets — this is because the count parameter retrieves that many tweets before filtering out retweets and replies. This parameter is only supported for JSON and XML responses</param>
		/// <param name="contributor_details">This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included</param>
		/// <param name="include_rts">When set to false, the timeline will strip any native retweets (though they will still count toward both the maximal length of the timeline and the slice selected by the count parameter). Note: If you’re using the trim_user parameter in conjunction with include_rts, the retweets will still contain a full user object</param>
		/// <remarks>https://dev.twitter.com/rest/reference/get/statuses/user_timeline</remarks>
		public static Task<HttpResponseMessage> GetUserTimelineAsync(
			this HttpClient client,
			ICredential consumerCredential,
			ICredential accessToken,
			int? user_id = null,
			string screen_name = null,
			int? since_id = null,
			int? count = null,
			int? max_id = null,
			bool trim_user = false,
			bool exclude_replies = false,
			bool contributor_details = false,
			bool include_rts = true)
		{
			var parameters = new Dictionary<string, string>();

			#region Add Parameters
			if (user_id.HasValue) parameters.Add(nameof(user_id), user_id.Value.ToString());
			if (screen_name != null) parameters.Add(nameof(screen_name), screen_name);
			if (since_id.HasValue) parameters.Add(nameof(since_id), since_id.Value.ToString());
			if (count.HasValue) parameters.Add(nameof(count), count.Value.ToString());
			if (max_id.HasValue) parameters.Add(nameof(max_id), max_id.Value.ToString());
			if (trim_user) parameters.Add(nameof(trim_user), trim_user.ToString());
			if (exclude_replies) parameters.Add(nameof(exclude_replies), exclude_replies.ToString());
			if (contributor_details) parameters.Add(nameof(contributor_details), contributor_details.ToString());
			if (!include_rts) parameters.Add(nameof(include_rts), include_rts.ToString());
			#endregion

			var uri = BuildUri("user_timeline.json", parameters);
			return client.AccessResourceAsync(
				uri,
				HttpMethod.Get,
				consumerCredential,
				accessToken,
				parameters);
		}
	}
}
