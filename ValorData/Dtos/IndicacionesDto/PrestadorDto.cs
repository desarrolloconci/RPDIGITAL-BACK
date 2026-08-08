namespace ValorModels.Dtos.IndicacionesDto
{
    // Mapea panet_Prestadores_BuscarPorID / panet_Prestadores_BuscarPorPermisosUsuarioLogeado
    // (ambos hacen SELECT * o p.* sobre Prestadores). Se omiten a proposito columnas de
    // credenciales/datos bancarios que ese SELECT * trae pero que no deben viajar por la API:
    // UsuarioParaWs, PassParaWs (credenciales de webservice), Pre_cbu, Pre_cbuAlias (cuenta
    // bancaria) - mismo criterio que Usuario_pass en UsuarioGeclisaDto.
    public class PrestadorDto
    {
        public int pre_id { get; set; }
        public string? pre_nombre { get; set; }
        public string? pre_dir { get; set; }
        public int? loc_id { get; set; }
        public int? pre_cpostal { get; set; }
        public string? pre_tel { get; set; }
        public string? pre_cel { get; set; }
        public int? pre_matn { get; set; }
        public int? pre_matp { get; set; }
        public int? pre_cod { get; set; }
        public int? tp_id { get; set; }
        public string? pre_email { get; set; }
        public string? pre_observaciones { get; set; }
        public int? np_id { get; set; }
    }
}
