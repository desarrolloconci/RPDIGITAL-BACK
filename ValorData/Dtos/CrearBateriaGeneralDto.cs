using System.Collections.Generic;

namespace ValorModels.Dtos
{
    public class CrearBateriaGeneralDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<int> EstudioIds { get; set; }
        public List<int> UsuarioIds { get; set; }
        public bool Publica { get; set; }
    }
}
