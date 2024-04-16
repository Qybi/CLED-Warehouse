using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.Warehouse.Authentication.Utils.Abstractions;

public interface IHashingUtils
{
	(string Salt, string PasswordHash) GetHashedPassword(string password, string salt = null);
	byte[] GenerateSalt();
}
