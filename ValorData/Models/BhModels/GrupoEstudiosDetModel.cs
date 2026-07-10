using System;

namespace ValorModels.Models.BhModels
{
    public class GrupoEstudiosDetModel
    {
        public int ID { get; set; }
        public int GRUPO_ID { get; set; }
        public int ESTUDIO_ID { get; set; }
        public bool? BAJA { get; set; }
        public DateTime? CREADO { get; set; }
        public string? USUARIO { get; set; }
    }
}
