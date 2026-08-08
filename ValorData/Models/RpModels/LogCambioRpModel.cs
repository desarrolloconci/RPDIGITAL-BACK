using System;

namespace ValorModels.Models.RpModels
{
    public class LogCambioRpModel
    {
        public int id { get; set; }
        public string idPedido { get; set; }
        public string? idEstudio { get; set; }
        public string accion { get; set; }
        public string? campo { get; set; }
        public string? valorAnterior { get; set; }
        public string? valorNuevo { get; set; }
        public int? idTurno { get; set; }
        public string? usuario { get; set; }
        public DateTime fecha { get; set; }
    }
}
