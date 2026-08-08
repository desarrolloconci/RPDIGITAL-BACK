namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea el resultset de pa_AteListarPorRangoFechaFichaArea. El SP devuelve columnas
    // distintas segun @Area: "Servicio" solo viene poblado cuando Area='A' (ambulatorio);
    // para Area='I' (internado) esa columna no existe en el resultset y queda null aca
    // (Dapper no la mapea, no es un bug).
    public class AtePorRangoDto
    {
        public int me_id { get; set; }
        public DateTime me_fecha { get; set; }
        public string? me_hs { get; set; }
        public string? Servicio { get; set; }
        public DateTime? me_fechaEgr { get; set; }
        public string? me_hsEgr { get; set; }
        public string? FechaHoraIngreso { get; set; }
        public string? FechaHoraEgreso { get; set; }
        public string? osplan { get; set; }
        public int? PedEstE_id { get; set; }
    }
}
