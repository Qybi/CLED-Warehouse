using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.WareHouse.Models.Login;

public class RegisterAttempt
{
	public string Username { get; set; } = null!;
	public string Password { get; set; } = null!;
	public List<string> Roles { get; set; } = null!;
	public bool Enabled { get; set; }
}
