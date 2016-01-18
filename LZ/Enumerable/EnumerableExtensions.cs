using System;
using System.Collections.Generic;
using System.Text;

namespace LZ.Enumerable
{
    public static class EnumerableExtensions
	{
		/// <summary>
		/// Joins an enumeration to a string which can be delimited.
		/// </summary>
		/// <param name="delimeter">The string inserted between in item.</param>
		/// <param name="itemConverter">A delegate that converts each item into a string. Otherwise "ToString()" is used.</param>
		/// <returns>Resulting joined string</returns>
		public static string JoinToString<T>(this IEnumerable<T> enumerable, string delimeter = null, Func<T, string> itemConverter = null)
		{
			if (enumerable == null) throw new NullReferenceException();

			StringBuilder stringBuilder = null;
			bool isFirst = true;

			foreach (T item in enumerable)
			{
				if (isFirst)
				{
					stringBuilder = new StringBuilder();
					isFirst = false;
				}
				else if (delimeter != null)
				{
					stringBuilder.Append(delimeter);
				}

				if (itemConverter == null)
				{
					stringBuilder.Append(item);
				}
				else
				{
					stringBuilder.Append(itemConverter(item));
				}
			}

			return stringBuilder == null ? String.Empty : stringBuilder.ToString();
		}
	}
}