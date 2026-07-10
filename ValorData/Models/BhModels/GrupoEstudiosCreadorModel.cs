using System;

namespace ValorModels.Models.BhModels
{
    public class GrupoEstudiosCreadorModel
    {
        public int ID { get; set; }
        public int GRUPO_ID { get; set; }
        public int USUARIO_ID { get; set; }
        public DateTime? CREADO { get; set; }
    }
}
