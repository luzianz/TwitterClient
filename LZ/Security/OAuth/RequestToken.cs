namespace LZ.Security.OAuth
{
	public class RequestToken : Credential
	{
		public RequestToken(string key, string secret) : base(key, secret) { }
		
		#region Properties

		public bool IsCallbackConfirmed { get; set; }

		#endregion
	}
}