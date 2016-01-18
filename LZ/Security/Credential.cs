namespace LZ.Security
{
	public class Credential : ICredential
	{
		public Credential(string key, string secret)
		{
			this.Key = key;
			this.Secret = secret;
		}

		public virtual string Key
		{
			get;
			protected set;
		}

		public string Secret
		{
			get;
			protected set;
		}
	}
}