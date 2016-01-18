using LZ.Format.Web;

namespace LZ.Security.OAuth
{
	internal static class Format
	{
		public static string GetSigningKey(string consumerSecret, string token)
		{
			return $"{consumerSecret.PercentEncode()}&{token.PercentEncode()}";
		}
	}
}