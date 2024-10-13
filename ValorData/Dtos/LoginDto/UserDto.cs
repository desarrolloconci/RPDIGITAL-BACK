using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.LoginDto
{
    public class UserDto
    {
      
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
       
    }
}
