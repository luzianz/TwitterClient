namespace LZ.Security.OAuth
{
	internal struct AuthorizationResult
	{
		public bool IsCallbackConfirmed { get; set; }
		public string Key { get; set; }
		public string Secret { get; set; }
	}
}