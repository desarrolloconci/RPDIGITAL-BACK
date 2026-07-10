using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class BhBateriasEstudiosModel
    {
        public int ID { get; set; }
        public int GRUPO_ID { get; set; }
        public string? GRUPO { get; set; }
        public int ESTUDIO_ID { get; set; }
        public string? ESTUDIO { get; set; }
        public bool? baja { get; set; }
        public int? METODO_ID { get; set; }
        public string? CODIGO { get; set; }
        public string? METODO { get; set; }
        public int? DISPONIBLE_TW { get; set; }
    }
}
