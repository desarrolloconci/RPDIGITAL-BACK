namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 el resultset de panet_HistoriaClinica_ListaReceta.
    // "Prestador" ya viene formateado como texto por el propio SP ("matp - nombre"),
    // no necesita enriquecimiento aparte.
    public class RecetaListadoDto
    {
        public int RecEnca_id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Prestador { get; set; }
        public string? firmado { get; set; }
        public string? diagnostico { get; set; }
        public DateTime Fec_Alta { get; set; }
        public DateTime? Fec_Modi { get; set; }
        public string? Paciente { get; set; }
        public string? Rec_Identificador { get; set; }
    }
}
