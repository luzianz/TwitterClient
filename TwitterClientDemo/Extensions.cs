using System;
using LZ.Security;
using Microsoft.Extensions.Configuration;

namespace TwitterClientDemo
{
	public static class Extensions
	{
		public static bool TryGetCredential(this IConfiguration configuration, string key, out ICredential credential)
		{
			var section = configuration.GetSection(key);
			if (section["key"] != null && section["secret"] != null)
			{
				credential = new Credential(section["key"], section["secret"]);
				return true;
			}
			else
			{
				credential = null;
				return false;
			}
		}
	}
}