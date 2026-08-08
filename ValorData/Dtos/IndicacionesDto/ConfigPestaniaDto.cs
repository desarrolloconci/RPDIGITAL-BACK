namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 el resultset de panet_ConfigPestaniasHistoriaClinicaInternado_ObtenerPorCodigo.
    // No depende de ficha_id - es config general de la app, se resuelve una vez por request.
    public class ConfigPestaniaDto
    {
        public int conf_id { get; set; }
        public string? conf_codigo { get; set; }
        public string? conf_nombre { get; set; }
        public bool? imprimen_enfermeros { get; set; }
        public bool? imprimen_prestadores { get; set; }
        public bool? imprimen_otros { get; set; }
        public bool? restringeFechaFutura { get; set; }
        public bool? restringeFechaAnterior { get; set; }
    }
}
