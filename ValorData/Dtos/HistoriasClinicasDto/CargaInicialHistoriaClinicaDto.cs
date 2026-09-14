namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Respuesta combinada de la Fase 1 (carga inicial de HC): agrupa las 3 variantes
    // de listado bajo un solo endpoint REST, disparadas en paralelo. No se fusionan
    // columnas entre variantes: cada una conserva su resultset original.
    public class CargaInicialHistoriaClinicaDto
    {
        public List<HistoriaClinicaListadoTevCodDto> ListadoTevCod { get; set; } = new();
        public List<HistoriaClinicaListadoSoloPantallasDto> ListadoSoloPantallas { get; set; } = new();
        public List<HistoriaClinicaListadoDto> Listado { get; set; } = new();
    }
}
