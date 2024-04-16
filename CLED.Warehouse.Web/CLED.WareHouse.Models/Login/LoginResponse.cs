using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.WareHouse.Models.Login;

public class LoginResponse
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public LoginStatus LoginStatus { get; set; }
    public UserInfo User { get; set; }
    public Credentials Credentials { get; set; }
}

public enum LoginStatus
{
	Successful,
	Unauthorized,
	Locked,
	Invalid
}

