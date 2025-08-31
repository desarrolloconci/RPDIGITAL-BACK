using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
     public class SegCantContactosModel
    {
        public int id { get; set; }
        public string idPedido { get; set; }
        public string idEstudio { get; set; }
        public int cantidad { get; set; }
        public int id_usuario { get; set; }
        public string MetodoOK { get; set; }
        public DateOnly fecha { get; set; }
    }
}
