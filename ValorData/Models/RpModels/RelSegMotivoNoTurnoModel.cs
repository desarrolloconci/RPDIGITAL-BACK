using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
   public class RelSegMotivoNoTurnoModel
    {
        public int id { get; set; }
        public string idPedido { get; set; }
        public string idEstudio { get; set; }
        public int id_motivo_no_turno { get; set; }
        public int id_usuario { get; set; }
        public string Metodo { get; set; }
        public DateOnly fecha { get; set; }
    }
}
