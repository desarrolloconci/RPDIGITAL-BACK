using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class AsignacionEstadoProgramaModel
    {
        public int id { get; set; }
        public  string dni { get; set; }
        public string unidad { get; set; }
        public int estado_id{ get; set; }
        public string usuario { get; set; }
        public DateOnly creado { get; set; }
    }
}
