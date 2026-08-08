namespace ValorModels.Dtos.IndicacionesDto
{
    // Respuesta de la carga inicial del tab Indicaciones/Recetas (Epic 3). Agrupa las 6
    // consultas de listado bajo un solo endpoint, disparadas en paralelo, y resuelve el
    // enriquecimiento Prestador/Usuario/Especialidad de las indicaciones con dedupe +
    // paralelismo (mismo patron que Epic 2), en vez de 1 llamada por item como hacia el
    // cliente actual (visto en la traza: pre_id=144 pedido 2 veces seguidas).
    public class CargaInicialIndicacionesDto
    {
        public List<RecetaListadoDto> Recetas { get; set; } = new();
        public List<AtePorRangoDto> Atenciones { get; set; } = new();
        public List<IndicacionMedicaDto> Indicaciones { get; set; } = new();
        public List<PedidoEstudioEncaDto> PedidosEstudios { get; set; } = new();
        public ConfigPestaniaDto? Config { get; set; }
        public List<HistoriaClinicaArchivoDto> Archivos { get; set; } = new();
        public List<PrestadorDto> PrestadoresPermitidos { get; set; } = new();

        // Enriquecimiento deduplicado de las Indicaciones (pre_id -> Prestador, usu_alta -> Usuario,
        // esp_id -> Especialidad). Si un id no se pudo resolver, no aparece en el diccionario
        // (fail-soft, igual que Laboratorio).
        public Dictionary<int, PrestadorDto> PrestadoresPorId { get; set; } = new();
        public Dictionary<string, ValorModels.Dtos.HistoriasClinicasDto.UsuarioGeclisaDto> UsuariosPorNombre { get; set; } = new();
        public Dictionary<int, EspecialidadDto> EspecialidadesPorId { get; set; } = new();
    }
}
