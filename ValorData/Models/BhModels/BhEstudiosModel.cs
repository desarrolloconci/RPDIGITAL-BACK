using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class BhEstudiosModel
    {
        public int ID { get; set; }
        public string ESTUDIO_CODIGO { get; set; }
        public string? ESTUDIO_NOMBRE { get; set; }
        public int METODO_ID { get; set; }
        public string? METODO_CODIGO { get; set; }
        public string? METODO_NOMBRE { get; set; }
        public int? CANTIDAD { get; set; }
        public int? DISPONIBLE_TW { get; set; }

        // Campos nuevos (resueltos contra PCHIM.ESTUDIOS_MASTER en SQL-02) en paralelo
        // a los viejos, para validar antes de reemplazarlos (ver plan 1.2/1.8). [NotMapped]
        // porque esta clase también es la entidad de EF mapeada a V_BH_ESTUDIOS (que no
        // tiene estas columnas) — se completan en C# después de traer los datos.
        [NotMapped]
        public int? ESTUDIO_ID_NUEVO { get; set; }
        [NotMapped]
        public string? ESTUDIO_NOMBRE_NUEVO { get; set; }
        [NotMapped]
        public int? METODO_ID_NUEVO { get; set; }
    }
}
