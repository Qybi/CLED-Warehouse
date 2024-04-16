using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.WareHouse.Models.Login;

public class Credentials
{
    public string Token { get; set; }
    public DateTime TokenExpDate { get; set; }
    public string Username { get; set; }
}
