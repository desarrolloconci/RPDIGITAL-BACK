using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class GrupoEstudiosModel
    {
        public int id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Habilitada { get; set; }
        public int? Publica { get; set; }
        public bool? Baja { get; set; }

        // No es una columna real de GRUPOESTUDIOS: se completa en el Cmd cruzando con GRUPOESTUDIOS_CREADOR.
        [NotMapped]
        public int? CreadoPorUsuarioId { get; set; }
    }
}
