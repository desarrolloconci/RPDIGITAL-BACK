using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
    public class SegObservacionesModel
    {
        public int id { get; set; }
        public string idPedido { get; set; }
        public string idEstudio { get; set; }
        public int idUsuario { get; set; }
        public string Metodo { get; set; }
        public string observacion { get; set; }
        public DateOnly Fecha { get; set; }
    }
}
