using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class RelSolPractModel
    {
        public int id { get; set; }
        public string idPedido { get; set; }
        public string idEstudio { get; set; }
        public string estadoPrograma { get; set; }
        public string estadoTurno { get; set; }
        public DateOnly fechaGestion { get; set; }
        public string observaciones { get; set; }
        public DateOnly creado { get; set; }
        public string usuario { get; set; }
        public int ? turno_id { get; set; }

    }
}
