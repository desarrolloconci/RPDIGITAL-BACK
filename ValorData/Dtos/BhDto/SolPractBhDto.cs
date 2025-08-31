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
        public DateOnly? fechaGestion { get; set; }
        public string? observaciones { get; set; }
        public DateOnly? creado { get; set; }
        public string? usuario { get; set; }
        public string? ESTUDIO { get; set; }
        public int? turno_id { get; set; }
        public string? METODOOK { get; set; }
        public string? unidad { get; set; }
        public string? servicio { get; set; }
        public int? CONFESPECIAL { get; set; }
        public string? INDUCTOR { get; set; }
        public string? Estado_pedido { get; set; }
        public int ATENDIDO { get; set; }
        public string UNIDAD_NOMBRE { get; set; }
        public int estado_Programa { get; set; }
        public DateTime? tur_fecha { get; set; }
        public string? OSCOD { get; set; }
        public int? INDUCTOR_ID { get; set; }
        public int Estado_Turno_id { get; set; }
        public string? Estado_Turno { get; set; }
        public string? NUMEROAFILIADO { get; set; }

        public string? CELULAR { get; set; }

        public string? EMAIL { get; set; }
        public string? motivo_no_turno { get; set; }
        public string? seguimiento_estado_turno { get; set; }
        public int? seguimiento_cantidad_contactos { get; set; }
        public string? METODOOK2 { get; set; }
        public string DIAGNÓSTICO { get; set; }
    }
}
