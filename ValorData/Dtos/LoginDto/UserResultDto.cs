using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.LoginDto
{
    public class UserResultDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string User_name { get; set; }
        public string Role { get; set; }
        public string? Matricula { get; set; }
        public string? Firma { get; set; }
        public int? GeclisaUsuarioId { get; set; }
        public List<string> EspecialidadesIds { get; set; } = new List<string>();
        public List<int> Matriculas { get; set; } = new List<int>();
        public List<int> BateriasIds { get; set; } = new List<int>();

    }
}
