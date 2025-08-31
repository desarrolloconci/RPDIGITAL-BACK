using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.LoginDto
{
    public class ValidateLoginDto
    {  
        public int ID { get; set; }   
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public string? FIRMA { get; set; }
    }
}
