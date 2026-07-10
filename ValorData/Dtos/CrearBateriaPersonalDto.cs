using System.Collections.Generic;

namespace ValorModels.Dtos
{
    public class CrearBateriaPersonalDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<int> EstudioIds { get; set; }
        public int UsuarioId { get; set; }
    }
}
