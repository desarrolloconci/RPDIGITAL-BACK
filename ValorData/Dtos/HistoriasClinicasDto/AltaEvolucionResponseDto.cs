namespace ValorModels.Dtos.HistoriasClinicasDto
{
    public class AltaEvolucionResponseDto
    {
        public int HcId { get; set; }
        public int? LogHcId { get; set; }
        public bool YaEstabaInternado { get; set; }
        public int? MeIdInternacion { get; set; }
    }
}
