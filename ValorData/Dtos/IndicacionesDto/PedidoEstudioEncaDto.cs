namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 el resultset de panet_PedidosEstudiosEnca_ConsultarPorMeId_o_FichaId.
    public class PedidoEstudioEncaDto
    {
        public int PedEstE_id { get; set; }
        public string? Paciente { get; set; }
        public string? Prestador { get; set; }
        public string? Serv_Nombre { get; set; }
        public string? PedEstE_Obs { get; set; }
        public string? Estado { get; set; }
        public string? RealizadoTXT { get; set; }
        public bool Realizado { get; set; }
        public int? Me_id { get; set; }
        public DateTime PedEstE_Fecha { get; set; }
        public string? PedEstE_Hora { get; set; }
        public int? Pre_id { get; set; }
        public int? Sol_id { get; set; }
        public int? margenTiempoMinutos { get; set; }
        public string? Observaciones { get; set; }
        public string? ColorSemaforo { get; set; }
        public string? AutorizadoTXT { get; set; }
        public string? MotivoRechazo { get; set; }
    }
}
