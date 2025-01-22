using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class EstadoPedidoManualModel
    {
        public int id { get; set; }
        public string idpedido { get; set; }
        public string idtratamiento {  get; set; }
        public bool enproceso { get; set; }
        public bool nocontactado { get; set; }
        public string usuario { get; set; }
        public DateOnly fecha { get; set; }
    }
}
