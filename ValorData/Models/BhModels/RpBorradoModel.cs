using System;

namespace ValorModels.Models.BhModels
{
    // Espejo de SolPractBhPedidoManualModel (tabla RP_BORRADOS): cuando se borra un pedido desde
    // /rp/hechos, en vez de perderlo se mueve aca, para poder seguir viendolo (y su historial de
    // cambios en SEG_LOG_CAMBIOS_RP, que sigue atado al mismo IDPEDIDO) desde /rp/borrados.
    public class RpBorradoModel
    {
        public int ID { get; set; }
        public string IDPEDIDO { get; set; }
        public string? IDTRATAMIENTO { get; set; }
        public string? NOMBREBATERIA { get; set; }
        public DateTime? FECHA { get; set; }
        public DateTime? FECHACREACION { get; set; }
        public string? DIAGNÓSTICO { get; set; }
        public string? METODOPRACTICA { get; set; }
        public string? IDESTUDIO { get; set; }
        public int? IDESTUDIO_NUM { get; set; }
        public string? ESTUDIO { get; set; }
        public string? DNI { get; set; }
        public string? NOMBRE { get; set; }
        public string? IDOBRASOCIAL { get; set; }
        public string? OBRASOCIAL { get; set; }
        public string? NUMEROAFILIADO { get; set; }
        public string? CELULAR { get; set; }
        public string? EMAIL { get; set; }
        public string? CODIGOPRESTADOR { get; set; }
        public string? PRESTADORQUEGENERASOLICITUD { get; set; }
        public string? IDTURNO { get; set; }
        public DateTime? FECHAHORA { get; set; }
        public DateTime? FECHAHORAALTA { get; set; }
        public DateTime? FECHAHORAGESTIONDEESTADO { get; set; }
        public string? USUARIOALTA { get; set; }
        public string? IDSERVICIOTURNO { get; set; }
        public string? SERVICIOTURNO { get; set; }
        public string? IDSERVICIOSOLICITUD { get; set; }
        public string? SERVICIOSOLICITUD { get; set; }
        public string? CODIGOPRESTADORDELTURNO { get; set; }
        public string? PRESTADORDELTURNO { get; set; }
        public DateTime? FECHAHORACONF { get; set; }
        public DateTime? FECHAHORAATENCION { get; set; }
        public string? ESTADO { get; set; }
        public string? USUARIOGESTIONOESTADO { get; set; }
        public string? CONTACTACION { get; set; }
        public string? MOTIVONOTURNO { get; set; }
        public string? OBSERVACIONES { get; set; }
        public DateTime? CREADO { get; set; }
        public string? USUARIO { get; set; }
        public int? Nro_Atencion { get; set; }
        public bool? No_gestion { get; set; }
        public int? Id_servicio_atencion { get; set; }
        public string? OBSERVACION_INTERNA { get; set; }

        public DateTime fechaBorrado { get; set; }
        public string? usuarioBorro { get; set; }
        public string? motivoBorrado { get; set; }
    }
}
