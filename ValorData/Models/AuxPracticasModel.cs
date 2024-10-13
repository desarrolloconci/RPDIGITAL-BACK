using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class AuxPracticasModel
    {
        public int ID { get; set; }
        public int ESTUDIO_ID { get; set; }
        public string? ESTUDIO_NOMBRE { get; set; }
        public int PRACTICA_ID { get; set; }
        public string? PRACTICA_NOMBRE { get; set; }
        public string? PRACTICA_CODIGO { get; set; }
        public string? PRACTICA_CODNOM { get; set; }

    }
}
