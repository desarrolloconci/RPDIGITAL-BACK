namespace ValorModels.Dtos.LaboratorioDto
{
    // Combina 1 determinacion (LaboratorioResultadoDto) con sus N valores historicos
    // (LaboratorioValorDto) - reemplaza el loop de 63 llamadas secuenciales del cliente actual
    // por un unico objeto ya armado del lado del servidor.
    public class LaboratorioDeterminacionDto
    {
        public LaboratorioResultadoDto Determinacion { get; set; } = null!;
        public List<LaboratorioValorDto> Valores { get; set; } = new();
    }
}
