namespace ValorModels.Dtos.ModulosDto
{
    // Mapea 1:1 el resultset de pa_AteAmb_MovPraMovPreDet. Varias columnas (MPre_Pret,
    // MPre_ivat, mp_ivaporct, mp_osporct, mpre_ivaporct, CodigoNom, Mp_diaMes,
    // esModuloPrincipalT) vienen pre-formateadas como texto por el propio SP (padding/concat
    // para mostrar en pantalla) - no son los valores numericos crudos, son el mismo texto que
    // ya generaba GECLISA.
    public class ModuloDetalleDto
    {
        public string? tp_nom { get; set; }
        public string? nom_cod { get; set; }
        public string? nom_nom { get; set; }
        public string? conc_nombre { get; set; }
        public string? pre_Nombre { get; set; }
        public decimal? mp_can { get; set; }
        public string? pre_Matp { get; set; }
        public string? MPre_Pret { get; set; }
        public string? MPre_ivat { get; set; }
        public string? MPre_PrePorct { get; set; }
        public string? mp_ivaporct { get; set; }
        public string? mp_osporct { get; set; }
        public decimal? MPre_Pre { get; set; }
        public decimal? MPre_Iva { get; set; }
        public decimal? mp_ivaporc { get; set; }
        public int me_id { get; set; }
        public int mp_id { get; set; }
        public int? nom_id { get; set; }
        public int? conc_id { get; set; }
        public int? pre_id { get; set; }
        public decimal? mp_osporc { get; set; }
        public decimal? MPre_PrePorc { get; set; }
        public string? nroinfo { get; set; }
        public DateTime? fechainfo { get; set; }
        public bool? esModuloPrincipal { get; set; }
        public DateTime mp_fecha { get; set; }
        public string? esModuloPrincipalT { get; set; }
        public string? CodigoNom { get; set; }
        public string? Mp_diaMes { get; set; }
        public string? mpre_ivaporct { get; set; }
        public string? Usu_alta { get; set; }
        public DateTime Fec_Alta { get; set; }
        public string? Usu_Modi { get; set; }
        public DateTime? Fec_Modi { get; set; }
        public int? Serv_id { get; set; }
        public string? Serv_Nombre { get; set; }
    }
}
