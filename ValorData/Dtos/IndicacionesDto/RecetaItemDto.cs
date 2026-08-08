namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 el resultset de panet_HistoriaClinica_DetalleReceta.
    public class RecetaItemDto
    {
        public int RecItem_id { get; set; }
        public string? Generico { get; set; }
        public string? Presentacion { get; set; }
        public int? Cant { get; set; }
        public string? Sugerencia { get; set; }
        public string? Frecuencia { get; set; }
        public string? Observaciones { get; set; }
        public decimal? Nroreg { get; set; }
    }
}
