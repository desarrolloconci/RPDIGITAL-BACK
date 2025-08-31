using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.BhDto
{
    public class SolPractBhRpDto
    {
        public int ID { get; set; }
        public string IDPEDIDO { get; set; }
        public string IDTRATAMIENTO { get; set; }
        public string NOMBREBATERIA { get; set; }
        public DateTime FECHA { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string DIAGNÓSTICO { get; set; }
        public string? METODOOK { get; set; }
        public string IDESTUDIO { get; set; }
        public string ESTUDIO { get; set; }
        public string DNI { get; set; }
        public string NOMBRE { get; set; }
        public string IDOBRASOCIAL { get; set; }
        public string OBRASOCIAL { get; set; }
        public string NUMEROAFILIADO { get; set; }
        public string CELULAR { get; set; }
        public string EMAIL { get; set; }
        public string CODIGOPRESTADOR { get; set; }
        public string PRESTADORQUEGENERASOLICITUD { get; set; }
        public string IDTURNO { get; set; }
        public DateTime? FECHAHORA { get; set; }
        public DateTime? FECHAHORAALTA { get; set; }
        public DateTime? FECHAHORAGESTIONDEESTADO { get; set; }
        public string USUARIOALTA { get; set; }
        public string IDSERVICIOTURNO { get; set; }
        public string SERVICIOTURNO { get; set; }
        public string? IDSERVICIOSOLICITUD { get; set; }
        public string? SERVICIOSOLICITUD { get; set; }
        public string CODIGOPRESTADORDELTURNO { get; set; }
        public string PRESTADORDELTURNO { get; set; }
        public DateTime? FECHAHORACONF { get; set; }
        public DateTime? FECHAHORAATENCION { get; set; }
        public string ESTADO { get; set; }
        public string USUARIOGESTIONOESTADO { get; set; }
        public string CONTACTACION { get; set; }
        public string MOTIVONOTURNO { get; set; }
        public string OBSERVACIONES { get; set; }
        public DateTime? CREADO { get; set; }
        public string USUARIO { get; set; }
        public string? UNIDAD { get; set; }
        public int? UNIDAD_ID { get; set; }
        public int CONFESPECIAL { get; set; }
        public string Estado_pedido { get; set; }
        public int ATENDIDO { get; set; }
        public string? INDUCTOR { get; set; }
        public int estado_Programa { get; set; }
        public string? OSCOD { get; set; }
        public int? INDUCTOR_ID { get; set; }
        public int Estado_Turno_id { get; set; }
        public int? id_motivo_no_turno { get; set; }
        public string? motivo_no_turno { get; set; }
        public string? seguimiento_estado_turno { get; set; }
        public int? seguimiento_cantidad_contactos { get; set; }
   
        public bool? No_gestion { get; set; }
        public string? FIRMA { get; set; }
    }
}
