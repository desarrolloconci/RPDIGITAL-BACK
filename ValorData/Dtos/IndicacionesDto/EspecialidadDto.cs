namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 el resultset de panet_Especialidades_BuscarPorID.
    public class EspecialidadDto
    {
        public int esp_id { get; set; }
        public string? esp_nombre { get; set; }
        public string? Cod_PamiSII { get; set; }
        public string? Cod_ConsultaPami { get; set; }
    }
}
