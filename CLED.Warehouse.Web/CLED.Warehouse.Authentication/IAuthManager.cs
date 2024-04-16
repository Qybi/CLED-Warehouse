using CLED.WareHouse.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.Warehouse.Authentication;

public interface IAuthManager
{
	Task<LoginResponse> LoginAsync(LoginAttempt loginAttempt);
	Task<UserInfo> GetUserAsync(int id);
	Task<UserInfo> GetUserAsync(string username);
}
