using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
    public class NnRelMotivoNoTurnoModel
    {
        public int id { get; set; }
        public string idPedido { get; set; }
        public string idEstudio { get; set; }
        public string metodo {  get; set; }
        public int idMotivo { get; set; }
        public int idUsuario { get; set; }
        public DateOnly Creado { get; set; }

    }
}
