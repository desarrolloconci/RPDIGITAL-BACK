using System.Collections.Generic;

namespace ValorModels.Dtos.LoginDto
{
    public class RegisterMedicoDto
    {
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Firma { get; set; }
        public string? Matricula { get; set; }
        public List<string> EspecialidadesIds { get; set; } = new List<string>();
        public List<int> Matriculas { get; set; } = new List<int>();
        public List<int> BateriasIds { get; set; } = new List<int>();
    }
}
