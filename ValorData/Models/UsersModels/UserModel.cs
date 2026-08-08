using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.UsersModels
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
        public string? FIRMA { get; set; }
        public string? Matricula { get; set; }
        // Usuario_id correspondiente en la tabla Usuarios de GECLISA (base separada, sin FK real).
        // Se asigna a mano en el alta/edicion elegido de un listado (ver UsuariosGeclisaController).
        public int? GeclisaUsuarioId { get; set; }
    }
}
