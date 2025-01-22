using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.BhDto
{
    public class SolPractBhDto
    {
        public int id { get; set; }
        public string DNI { get; set; }
        public string NOMBRE { get; set; }
        public string OBRASOCIAL { get; set; }
        public DateTime FECHA { get; set; }
        public string PRESTADORQUEGENERASOLICITUD { get; set; }
        public int? idrelsol { get; set; }
        public string idPedido { get; set; }
        public string idEstudio { get; set; }
        public string? estadoTurno { get; set; }
        public DateOnly? fechaGestion { get; set; }
        public string? observaciones { get; set; }
        public DateOnly? creado { get; set; }
        public string? usuario { get; set; }
        public string? ESTUDIO { get; set; }
        public int? turno_id { get; set; }
        public string? METODOOK { get; set; }
        public int? unidad { get; set; }
        public string? servicio { get; set; }
        public int? CONFESPECIAL { get; set; }
        public string? INDUCTOR { get; set; }
        public string Estado_pedido { get; set; }
        public int ATENDIDO { get; set; }
        public string UNIDAD_NOMBRE { get; set; }
        public int? estado_Programa { get; set; }
        public DateTime tur_fecha { get; set; }
    }
}
