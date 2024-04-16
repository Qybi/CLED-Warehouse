using CLED.Warehouse.Authentication.Utils.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CLED.Warehouse.Authentication.Utils;

public class HashingUtils : IHashingUtils
{
	// this stuff is straight up copied ahah xd
	public (string Salt, string PasswordHash) GetHashedPassword(string password, string salt = null)
	{
		byte[] saltByte;
		bool saltIsNull = salt is null;
		if (saltIsNull)
		{
			saltByte = GenerateSalt();
		}
		else
		{
			saltByte = Convert.FromBase64String(salt);
		}


		byte[] hashBytes = KeyDerivation.Pbkdf2(
			password: password,
			salt: saltByte,
			prf: KeyDerivationPrf.HMACSHA512,
			iterationCount: 50000,
			numBytesRequested: 512 / 8);

		return (saltIsNull ? Convert.ToBase64String(saltByte) : salt, Convert.ToBase64String(hashBytes));
	}

	public byte[] GenerateSalt()
	{
		byte[] saltByte = new byte[128 / 8];
		using (var rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(saltByte);
		}
		return saltByte;
	}
}
