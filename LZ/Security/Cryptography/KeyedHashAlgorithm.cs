#if NETCOREAPP1_1 || NETCOREAPP1_0
using System;
using C = System.Security.Cryptography;

namespace LZ.Security.Cryptography
{
	public class KeyedHashAlgorithm : IDisposable
	{
		private readonly C.KeyedHashAlgorithm alg;

		public KeyedHashAlgorithm(HashAlgorithmNames algorithm)
		{
			switch (algorithm)
			{
				case HashAlgorithmNames.Sha1:
					alg = new C.HMACSHA1();
					break;
				case HashAlgorithmNames.Sha256:
					alg = new C.HMACSHA256();
					break;
				case HashAlgorithmNames.Sha384:
					alg = new C.HMACSHA384();
					break;
				case HashAlgorithmNames.Sha512:
					alg = new C.HMACSHA512();
					break;
				case HashAlgorithmNames.Md5:
					alg = new C.HMACMD5();
					break;
				default:
					throw new NotSupportedException();
			}
		}

		public byte[] Key
		{
			get
			{
				return alg.Key;
			}
			set
			{
				alg.Key = value;
			}
		}

		public byte[] ComputeHash(byte[] data)
		{
			return alg.ComputeHash(data);
		}

		public void Dispose()
		{
			alg.Dispose();
		}
	}
}
#elif NETFX_CORE
using System;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace LZ.Security.Cryptography
{
	public class KeyedHashAlgorithm
	{
		private readonly MacAlgorithmProvider alg;

		public KeyedHashAlgorithm(HashAlgorithmNames algorithm)
		{
			switch(algorithm)
			{
				case HashAlgorithmNames.Sha1:
					alg = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
					break;
				case HashAlgorithmNames.Sha256:
					alg = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
					break;
				case HashAlgorithmNames.Sha384:
					alg = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha384);
					break;
				case HashAlgorithmNames.Sha512:
					alg = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha512);
					break;
				case HashAlgorithmNames.Md5:
					alg = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacMd5);
					break;
				default:
					throw new NotSupportedException();
			}
		}

		public byte[] Key { get; set; }

		public byte[] ComputeHash(byte[] data)
		{
			var dataBuffer = CryptographicBuffer.CreateFromByteArray(data);
			var keyBuffer = CryptographicBuffer.CreateFromByteArray(Key);

			var hash = alg.CreateHash(keyBuffer);
			hash.Append(dataBuffer);

			var resultBuffer = hash.GetValueAndReset();
			byte[] result = new byte[resultBuffer.Length];

			CryptographicBuffer.CopyToByteArray(resultBuffer, out result);

			return result;
		}
	}
}
#endif