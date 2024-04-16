using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.WareHouse.Models.Login;

public class UserInfo
{
    //public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Enabled { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
