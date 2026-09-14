namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea 1:1 el resultset de panet_HistoriasClinicas_BuscarPorFechaFichaPreIdSoloPantallas
    public class HistoriaClinicaListadoSoloPantallasDto
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
        public string? Usuario { get; set; }
        public string? UsuarioApeNom { get; set; }
        public byte[]? ImgFirma { get; set; }
        public int? tev_id { get; set; }
        public string? tev_nombre { get; set; }
        public int? pan_id { get; set; }
        public int? esp_id { get; set; }
        public string? esp_nombre { get; set; }
        public bool? ExcluirDeImpresionEvolucion { get; set; }
        public int? cama_id { get; set; }
        public string? NombreCama { get; set; }
        public int? sec_id { get; set; }
        public string? sec_nombre { get; set; }
        public int? Me_id { get; set; }
        public string? Edad { get; set; }
    }
}
