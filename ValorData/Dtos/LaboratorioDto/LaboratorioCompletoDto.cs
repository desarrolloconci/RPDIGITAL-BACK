namespace ValorModels.Dtos.LaboratorioDto
{
    // Respuesta del endpoint de Laboratorio en paralelo (Epic 2). Fail-soft: si algun det_id
    // individual falla, se reporta en ErroresParciales pero no se tumba toda la respuesta.
    public class LaboratorioCompletoDto
    {
        public List<LaboratorioDeterminacionDto> Determinaciones { get; set; } = new();
        public List<int> ErroresParciales { get; set; } = new();
    }
}
