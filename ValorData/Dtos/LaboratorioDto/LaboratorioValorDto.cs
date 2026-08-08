namespace ValorModels.Dtos.LaboratorioDto
{
    // Mapea el resultset de pa_HistoriaPrestacional_LaboratorioResultadoValorxDet (N filas por det_id).
    // Valor es string porque InfoLabDet.valor es tipo `text` en la base (no numerico) -
    // los resultados de laboratorio pueden ser cualitativos, no solo valores numericos.
    public class LaboratorioValorDto
    {
        public DateTime Mp_fecha { get; set; }
        public string? Valor { get; set; }
        public string? Met_Nom { get; set; }
        public string? cargaresultado { get; set; }
        public int? mp_hs { get; set; }
    }
}
