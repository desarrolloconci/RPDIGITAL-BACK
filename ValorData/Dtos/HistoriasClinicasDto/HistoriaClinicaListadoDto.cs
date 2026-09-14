namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea 1:1 el resultset de panet_HistoriasClinicas_BuscarPorFechaFichaPreId
    // (la variante "completa": incluye permisos de PrestadorPuedeVerEvolucion/EsSuperEditor
    // ya resueltos server-side por el SP, IdTratamiento y Me_area).
    public class HistoriaClinicaListadoDto
    {
        public int Hc_id { get; set; }
        public string? Hc_Fecha { get; set; }
        public string? Hc_Hs { get; set; }
        public string? Hc_HsConFormato { get; set; }
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
        public string? tev_cod { get; set; }
        public int? pan_id { get; set; }
        public int? esp_id { get; set; }
        public string? esp_nombre { get; set; }
        public bool? ExcluirDeImpresionEvolucion { get; set; }
        public int? cama_id { get; set; }
        public string? NombreCama { get; set; }
        public int? sec_id { get; set; }
        public string? sec_nombre { get; set; }
        public int? Me_id { get; set; }
        public string? Me_area { get; set; }
        public string? Edad { get; set; }
        public string? IdTratamiento { get; set; }
    }
}
