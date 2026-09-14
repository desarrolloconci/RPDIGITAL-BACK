namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea 1:1 el resultset de pa_HistoriaClinica_TraerEstudios (tabla temporal #MOVPRAC).
    // info_id/infoLab_id vienen en -1 o 0 segun el propio SP cuando no aplican (informe de
    // diagnostico vs. informe de laboratorio) - no es un bug de este mapeo.
    public class EstudioDto
    {
        public int me_id { get; set; }
        public string? serv_nombre { get; set; }
        public string? me_area { get; set; }
        public DateTime mp_fecha { get; set; }
        public string? tipoNom { get; set; }
        public string? nom_nom { get; set; }
        public int info_id { get; set; }
        public int infoLab_id { get; set; }
        public int mp_id { get; set; }
        public int AccessionNumber { get; set; }
    }
}
