using System.Collections.Generic;

namespace LZ.Format.Web
{
    internal class QueryParameterComparer : IComparer<KeyValuePair<string, string>>
	{
		#region IComparer<KeyValuePair<string, string>>

		public int Compare(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
		{
			if (x.Key == y.Key)
			{
				return string.Compare(x.Value, y.Value);
			}
			else
			{
				return string.Compare(x.Key, y.Key);
			}
		}

		#endregion
	}
}