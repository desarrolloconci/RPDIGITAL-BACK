using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class InstructivoModel
    {
        public int id { get; set; }
        public int ESTUDIO_ID { get; set; }
        public string  ESTUDIO_NOMBRE { get; set; }
        public string PREPARACION { get; set; }
        public int PLAZO_ID { get; set; }
        public string PLAZO_NOMBRE { get; set; }
        public int CRITERIO_ID { get; set; }
        public string CRITERIO_NOMBRE { get; set; }
        public string COMBINACION { get; set; }
        public string DIAGNOSTICOS { get; set; }
        public string WARNING { get; set; }
        public string INFORMACION { get; set; }
        public string LINK { get; set; }
        public string CONSENTIMIENTO { get; set; }
        public int TIPOENTREGA_ID { get; set; }
        public string TIPOENTREGA_NOMBRE { get; set; }
        public string FORMACION { get; set; }
        public bool BAJA { get; set; }
    }
}
     
   