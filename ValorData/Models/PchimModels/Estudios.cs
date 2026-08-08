namespace ValorModels.Models.PchimModels
{
    public class Estudios
    {
        public int ID { get; set; }
        public int ESTUDIO_ID { get; set; }
        public string? ESTUDIO_CODIGO { get; set; }
        public int METODO_ID { get; set; }
        public int? TIPO_ID { get; set; }
        public int? CANTIDAD { get; set; }
        public bool? CONTRASTE { get; set; }
        public string? CONTRASTE_EV { get; set; }
        public string? CONTRASTE_ORAL { get; set; }
        public string? ANATOMIA { get; set; }
        public string? MATERIALES { get; set; }
        public bool? DISPONIBLE_RP { get; set; }
        public bool? DISPONIBLE_TW { get; set; }
        public bool? DISPONIBLE_AS { get; set; }
        public bool? BONO_EMERGENCIA { get; set; }
        public bool? DIFERENCIA_PRECIO { get; set; }
        public int? PREPARACION_ID { get; set; }
        public bool? BAJA { get; set; }
        public string? NOMBRE { get; set; }
        public bool? DISPONIBLE_TOT { get; set; }
        public bool? DISPONIBLE_BOTRP { get; set; }
        public bool? DISPONIBLE_BOTCONTACT { get; set; }
        public bool? DIS_MATERIALES { get; set; }
        public bool? DISPONIBLE_AUTOGESTIONCARGA { get; set; }
        public bool? VALOR_POR_PREST { get; set; }
        public int? Particularidad_Cod { get; set; }
        public string? URLPREPARACION { get; set; }
        public bool? TwSiRp { get; set; }
    }
}
