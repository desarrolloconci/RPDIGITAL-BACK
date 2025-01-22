using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class BhEstudiosModel
    {
        public int ID { get; set; }
        public string ESTUDIO_CODIGO { get; set; }
        public string ESTUDIO_NOMBRE { get; set; }
        public int METODO_ID { get; set; }
        public string METODO_CODIGO { get; set; }
        public string METODO_NOMBRE { get; set; }
        public int CANTIDAD { get; set; }
    }
}
