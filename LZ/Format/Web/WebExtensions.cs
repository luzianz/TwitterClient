using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LZ.Format.Web
{
	using Conversion;
	using Enumerable;

	/// <summary>
	/// Extensions methods related to formatting uris
	/// </summary>
	public static class WebExtensions
	{
		/// <summary>
		/// Replaces the missing "Authority" property of a Uri in stripped-down versions of the .Net framework.
		/// </summary>
		public static string GetAuthority(this Uri uri)
		{
			if ((uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443))
			{
				return uri.Host;
			}
			else
			{
				return string.Format("{0}:{1}", uri.Host, uri.Port);
			}
		}

		public static bool IsReserved(this char c)
		{
			return c == '-'
				|| c == '.'
				|| c == '_'
				|| c == '~';
		}

		public static bool IsBetweenZeroAndNine(this char c)
		{
			return c >= '0' && c <= '9';
		}

		public static bool IsBetweenAandZ_Upper(this char c)
		{
			return c >= 'A' && c <= 'Z';
		}

		public static bool IsBetweenAandZ_Lower(this char c)
		{
			return c >= 'a' && c <= 'z';
		}

		public static bool RequiresEncoding(this char c)
		{
			return !(c.IsReserved()           // - . _ ~
				|| c.IsBetweenZeroAndNine()   // 0-9
				|| c.IsBetweenAandZ_Upper()   // A-Z
				|| c.IsBetweenAandZ_Lower()); // a-z
		}

		public static string ToNormalizedString(this Uri uri)
		{
			return string.Format("{0}://{1}{2}", uri.Scheme, uri.GetAuthority(), uri.AbsolutePath);
		}

		public static IEnumerable<KeyValuePair<string, string>> PercentEncodeKeysAndValues(this IEnumerable<KeyValuePair<string, string>> parameters, bool useCaps = true)
		{
			foreach (var p in parameters)
			{
				string encodedKey = p.Key.PercentEncode(useCaps);
				string encodedValue = p.Value.PercentEncode(useCaps);

				yield return new KeyValuePair<string, string>(encodedKey, encodedValue);
			}
		}

		public static string ToEncodedQueryString(this IEnumerable<KeyValuePair<string, string>> parameters, bool sort = false, bool useCaps = true)
		{
			var percentEncodedPairs = parameters.PercentEncodeKeysAndValues(useCaps);
			var organizedPercentEncodedPairs = sort ?
				percentEncodedPairs.OrderBy(p => p, new QueryParameterComparer()) :
				percentEncodedPairs;

			return organizedPercentEncodedPairs.JoinToString("&", p => string.Format("{0}={1}", p.Key, p.Value));
		}

		public static string PercentEncode(this string s, bool useCaps = true)
		{
			var stringBuilder = new StringBuilder();

			foreach (char c in s)
			{
				if (c.RequiresEncoding())
				{
					stringBuilder.Append(c.PercentEncode(useCaps));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString();
		}

		/// <remarks>refer to https://dev.twitter.com/docs/auth/percent-encoding-parameters </remarks>
		public static string PercentEncode(this char c, bool useCaps = true)
		{
			return string.Format("%{0}", c.ToHexadecimal(useCaps));
		}
	}
}