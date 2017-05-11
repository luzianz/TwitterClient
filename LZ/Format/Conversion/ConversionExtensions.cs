using System;
using System.Collections.Generic;
using System.Text;

namespace LZ.Format.Conversion
{
	public static class ConversionExtensions
	{
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Converts a DateTime to a unix timestamp
		/// </summary>
		public static int ToTimestamp(this DateTime dt)
		{
			return (int)dt.Subtract(epoch).TotalSeconds;
		}

		/// <summary>
		/// Converts a number from 0 to 15 to its hexadecimal representation
		/// </summary>
		/// <param name="b">A byte with a value ranging from 0 to 15</param>
		/// <param name="useCaps">If true, letters will be capitalized</param>
		/// <returns>A hexadecimal character from '0' to 'F'</returns>
		private static char ToHexadecimalDigit(byte b, bool useCaps = true)
		{
			switch (b)
			{
				case 0:
					return '0';
				case 1:
					return '1';
				case 2:
					return '2';
				case 3:
					return '3';
				case 4:
					return '4';
				case 5:
					return '5';
				case 6:
					return '6';
				case 7:
					return '7';
				case 8:
					return '8';
				case 9:
					return '9';
				case 10:
					return useCaps ? 'A' : 'a';
				case 11:
					return useCaps ? 'B' : 'b';
				case 12:
					return useCaps ? 'C' : 'c';
				case 13:
					return useCaps ? 'D' : 'd';
				case 14:
					return useCaps ? 'E' : 'e';
				case 15:
					return useCaps ? 'F' : 'f';
				default:
					throw new ArgumentOutOfRangeException("b", "must be a value between 0 and 15");
			}
		}

		public static string ToHexadecimal(this char c, bool useCaps = true)
		{
			return String.Format(
				"{0}{1}",
				ToHexadecimalDigit((byte)(c >> 4), useCaps),
				ToHexadecimalDigit((byte)(c % 16), useCaps)
			);
		}
		
		public static string ToHexadecimal(this IEnumerable<char> characters, bool useCaps = true)
		{
			var stringBuilder = new StringBuilder();

			foreach (char c in characters)
			{
				stringBuilder.Append(c.ToHexadecimal(useCaps));
			}

			return stringBuilder.ToString();
		}
	}
}
