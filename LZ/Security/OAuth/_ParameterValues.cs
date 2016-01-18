using LZ.Format.Conversion;
using System;
using System.Linq;

namespace LZ.Security.OAuth
{
	internal static class ParameterValues
	{
		#region Constants

		public const string OAuthVersion1 = "1.0";
		public const string HMAC_SHA1 = "HMAC-SHA1";

		#endregion

		public static string GenerateNonce()
		{
			return Guid.NewGuid().ToByteArray().Select(b => (char)b).ToHexadecimal(true);
		}

		public static string GetTimestamp()
		{
			return DateTime.UtcNow.ToTimestamp().ToString();
		}
	}
}
