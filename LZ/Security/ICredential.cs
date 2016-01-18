namespace LZ.Security
{
	public interface ICredential
	{
		/// <summary>
		/// Represents identity.
		/// </summary>
		string Key { get; }

		/// <summary>
		/// Represents a secret known only by its keeper (identified by Key)
		/// </summary>
		string Secret { get; }
	}
}