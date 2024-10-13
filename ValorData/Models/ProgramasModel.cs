using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class ProgramasModel
    {
        public int id { get; set; }
        public string Nombre_programa { get; set; }
        public bool activo { get; set; }
        public DateOnly creado { get; set; }
        public string usuario { get; set; }
    }
}
