namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 el resultset de panet_HistoriaClinicaArchivo_BuscarPorFechaFichaPreId.
    public class HistoriaClinicaArchivoDto
    {
        public int Hca_id { get; set; }
        public DateTime Hca_Fecha { get; set; }
        public string? Hca_Hs { get; set; }
        public int? Pre_Matp { get; set; }
        public string? Pre_Nombre { get; set; }
        public string? Observacion { get; set; }
        public int? Pre_id { get; set; }
        public int Ficha_id { get; set; }
        public string? Firmado { get; set; }
        public int? Me_id { get; set; }
        public DateTime Fec_Alta { get; set; }
        public DateTime? Fec_Modi { get; set; }
        public string? as_path { get; set; }
        public string? as_titulo { get; set; }
        public int as_id { get; set; }
        public string? ar_extension { get; set; }
        public string? ar_abrirConPrograma { get; set; }
        public bool? esAdministrativo { get; set; }
        public string? ac_nombre { get; set; }
    }
}
