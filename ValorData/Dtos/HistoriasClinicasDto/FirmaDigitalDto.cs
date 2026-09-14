namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea 1:1 el resultset de panet_FirmaDigital (0 o 1 fila: puede no haber firma)
    public class FirmaDigitalDto
    {
        public DateTime fechahora { get; set; }
        public string? firmante { get; set; }
        public string? pathcda { get; set; }
    }
}
