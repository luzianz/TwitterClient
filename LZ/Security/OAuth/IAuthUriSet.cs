using System;

namespace LZ.Security.OAuth
{
	public interface IAuthUriSet
	{
		Uri AuthorizeUri { get; }
		Uri RequestTokenUri { get; }
		Uri AccessTokenUri { get; }
	}
}