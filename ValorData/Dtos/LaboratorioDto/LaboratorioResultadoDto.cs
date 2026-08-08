namespace ValorModels.Dtos.LaboratorioDto
{
    // Mapea el resultset de pa_HistoriaPrestacional_LaboratorioResultado (lista de determinaciones).
    // infoLab_id/InfoLab_fecha/InfoLab_Hs/Serv_Nombre son placeholders fijos que el propio SP
    // devuelve siempre iguales (0 / '01/01/2000' / '00:00' / '') - no es un bug de este mapeo,
    // es el comportamiento real del SP (ver #tmp en su definicion).
    public class LaboratorioResultadoDto
    {
        public int Id { get; set; } // det_id
        public string? TipoNom { get; set; }
        public string? Código { get; set; }
        public string? Descripción { get; set; }
        public string? det_nombre { get; set; }
        public string? unidadMedida { get; set; }
        public string? valorNormal { get; set; }
        public int? Orden { get; set; }
        public int? infoLab_id { get; set; }
        public string? InfoLab_fecha { get; set; }
        public string? InfoLab_Hs { get; set; }
        public string? Serv_Nombre { get; set; }
    }
}
