using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.LoginDto
{
    public class LoginResponseDto
    {   
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string? FIRMA { get; set; }
    }
}
