using System;

namespace ValorModels.Models.RpModels
{
    // Tickets creados desde los botones "Falta un estudio"/"Falta una obra social" (webhook n8n ->
    // ticketera). Todo ticket nuevo entra como "Pendiente"; respuesta/fechaRespuesta se completan
    // mas adelante, cuando se conecte la consulta de estado contra la ticketera.
    public class TicketSoporteModel
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public string texto { get; set; }
        public string? usuario { get; set; }
        public DateTime fecha { get; set; }
        public string? ticketId { get; set; }
        public string? respuestaCruda { get; set; }
        public string estado { get; set; } = "Pendiente";
        public string? respuesta { get; set; }
        public DateTime? fechaRespuesta { get; set; }
    }
}
