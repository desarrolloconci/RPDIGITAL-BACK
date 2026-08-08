namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea 1:1 la tabla IndicacionesMedicas (panet_IndicacionesMedicas_ListarPorFicha hace
    // SELECT * sobre ella). pre_id/usu_alta/esp_id son las claves que se usan para el
    // enriquecimiento Prestador/Usuario/Especialidad (ver CargaInicialIndicacionesDto).
    public class IndicacionMedicaDto
    {
        public int im_id { get; set; }
        public int? pre_id { get; set; }
        public DateTime? fechaHora { get; set; }
        public DateTime? fec_alta { get; set; }
        public string? usu_alta { get; set; }
        public DateTime? fec_modi { get; set; }
        public string? usu_modi { get; set; }
        public DateTime? fec_baja { get; set; }
        public int? me_id { get; set; }
        public int ficha_id { get; set; }
        public string? OsPlan { get; set; }
        public int? plan_id { get; set; }
        public int? esp_id { get; set; }
        public string? Nro_Afiliado { get; set; }
        public string? im_textoAux { get; set; }
        public string? im_texto { get; set; }
    }
}
