using System.Collections.Generic;

namespace LZ.Security.OAuth
{
    internal static class TokenParser
	{
		public static bool TryParseQueryString(string queryString, out AuthorizationResult result)
		{
			try
			{
				var _token = new AuthorizationResult();
				var parameters = new Dictionary<string, string>();
				ParseQueryString(queryString, parameters);

				foreach (var kvp in parameters)
				{
					switch (kvp.Key)
					{
						case "oauth_token_secret":
						case "oauth_verifier":
							_token.Secret = parameters[kvp.Key];
							break;

						case "oauth_token":
							_token.Key = parameters[kvp.Key];
							break;

						case "oauth_callback_confirmed":
							_token.IsCallbackConfirmed = bool.Parse(parameters[kvp.Key]);
							break;
					}
				}
				result = _token;
				return true;
			}
			catch (KeyNotFoundException)
			{
				result = default(AuthorizationResult);
				return false;
			}
		}

        private static void ParseQueryString(string queryString, IDictionary<string, string> parameters)
        {
            string[] queryStringWithMaybeUriAndFragment = queryString.Split('#');
            // queryStringAndFragment[1] (fragment) is discarded (if exists)
            string[] queryStringWithMaybeUri = queryStringWithMaybeUriAndFragment[0].Split('?');
            string[] parameterCoupledStrings = queryStringWithMaybeUri[queryStringWithMaybeUri.Length == 2 ? 1 : 0].Split('&');

            foreach (string parameterCoupledString in parameterCoupledStrings)
            {
                string[] parameterPair = parameterCoupledString.Split('=');

                if (parameterPair.Length > 1)
                {
                    parameters.Add(parameterPair[0], parameterPair[1]);
                }
                else if (parameterPair.Length == 1)
                {
                    parameters.Add(parameterPair[0], null);
                }
            }
        }
    }
}