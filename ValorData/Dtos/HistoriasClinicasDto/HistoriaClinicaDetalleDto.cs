namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea 1:1 el resultset de panet_HistoriasClinicas_BuscarPorID
    public class HistoriaClinicaDetalleDto
    {
        public int Hc_id { get; set; }
        public DateTime Hc_Fecha { get; set; }
        public int? Hc_Hs { get; set; }
        public int Pre_id { get; set; }
        public int Ficha_id { get; set; }
        public string? Texto { get; set; }
        public string? Usu_Alta { get; set; }
        public DateTime Fec_Alta { get; set; }
        public string? Usu_Modi { get; set; }
        public DateTime? Fec_Modi { get; set; }
        public int? Me_id { get; set; }
        public int? Esp_id { get; set; }
        public int? np_id { get; set; }
    }
}
