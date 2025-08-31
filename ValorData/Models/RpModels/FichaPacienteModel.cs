using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
    public class FichaPacienteModel
    {
        public int id { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public int? nro_documento { get; set; }
        public DateOnly fecha_nac {  get; set; }
        public DateOnly fecha_alta { get; set; }
        public  string? email { get; set; }
    }
}
