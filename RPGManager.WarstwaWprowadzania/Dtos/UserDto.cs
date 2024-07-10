using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Dtos
{
    public class UserDto
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}