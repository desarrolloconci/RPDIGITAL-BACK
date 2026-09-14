namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea 1:1 el resultset de panet_HistoriasClinicas_BuscarPorFechaFichaPreIdTevCod
    public class HistoriaClinicaListadoTevCodDto
    {
        public int Hc_id { get; set; }
        public DateTime Hc_Fecha { get; set; }
        public string? Hc_Hs { get; set; }
        public int? Pre_Matp { get; set; }
        public string? Pre_Nombre { get; set; }
        public string? Texto { get; set; }
        public int Pre_id { get; set; }
        public int Ficha_id { get; set; }
        public string? firmado { get; set; }
        public int? esp_id { get; set; }
        public string? esp_nombre { get; set; }
        public bool? ExcluirDeImpresionEvolucion { get; set; }
    }
}
