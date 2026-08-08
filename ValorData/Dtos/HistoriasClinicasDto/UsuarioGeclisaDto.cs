namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Mapea el resultset de panet_Usuarios_BuscarPorID (tabla Usuarios de GECLISA,
    // distinta de la tabla de usuarios de Natanet). Info del usuario logueado (medico),
    // no de la nota/Hc_id que se este mirando.
    // Nota: el SP tambien devuelve Usuario_pass (hash de contraseña) - se omite a proposito,
    // no debe viajar por la API aunque el SP lo incluya en su resultset.
    public class UsuarioGeclisaDto
    {
        public int Usuario_id { get; set; }
        public string? Usuario_nom { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? Inicio { get; set; }
        public string? Maquina { get; set; }
        public decimal? Usuario_Suc { get; set; }
        public string? Usu_nom { get; set; }
        public string? Usu_Ape { get; set; }
        public bool? esCajero { get; set; }
        public bool? EditaInfoLab { get; set; }
        public bool? EditaCoseguroAmb { get; set; }
        public bool? EditaValoresEleAmb { get; set; }
        public bool? esReceptorHC { get; set; }
        public string? Usu_Mail { get; set; }
        // varchar(50) en la tabla, no bit - guarda datos de certificado digital, no un flag on/off
        public string? Certificado { get; set; }
        public bool? TieneToken { get; set; }
        public bool? esFirmante { get; set; }
        public bool? Usuario_EsEnfermero { get; set; }
        public string? Usuario_CodInterno { get; set; }
        public bool? Usuario_Inactivo { get; set; }
        public bool? UsaNuevaHc { get; set; }
        // smallint en la tabla, no bit
        public short? ImprimeHistoriaClinica { get; set; }
        public short? ImprimeEvolucionInternado { get; set; }
        public byte[]? ImgFirma { get; set; }
        public bool? BloqueaEdicionValoresElemInt { get; set; }
        public bool? SuperEditorHc { get; set; }
        public bool? SuperEditorEnfermero { get; set; }
        public short? TamañoFuenteHc { get; set; }
        public bool? EsPrestadorTecnico { get; set; }
        public bool? ModificaAutorValidadorInf { get; set; }
        public bool? EsFirmanteResLab { get; set; }
        public bool? UsaReconocimientoVoz { get; set; }
        public bool? NoProponerRestoUsuFirLab { get; set; }
        public bool? PreguntarSiValidaInformeAlGrabar { get; set; }
        public int? As_id { get; set; }
        public bool? GrabaRegPrivado { get; set; }
        public bool? EsAuditorHc { get; set; }
        public bool? NoValidaTopesGP { get; set; }
    }
}
