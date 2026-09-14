namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Respuesta combinada del detalle de "la nota actualmente seleccionada" (Fases 1,3,6,7,11
    // de la traza): BuscarPorID + FirmaDigital + BuscarPanId disparados para el mismo Hc_id.
    // No incluye Usuarios_BuscarPorID: ese es el usuario logueado, no depende del Hc_id
    // (ver endpoint separado GetUsuarioLogueado).
    public class NotaSeleccionadaDetalleDto
    {
        public HistoriaClinicaDetalleDto? Detalle { get; set; }
        public FirmaDigitalDto? Firma { get; set; }
        public int? PanId { get; set; }
    }
}
