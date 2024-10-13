using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos
{
    public class PracticasDto
    {
        public int Id { get; set; }
        public int Id_Programa { get; set; }
        public string Cod_practica { get; set; }
        public string Nombre_Practica { get; set; }
        public bool Opcional { get; set; }
        public bool Activo { get; set; }
        public string Nombre_programa { get; set; }
        public DateOnly Creado { get; set; }
        public string Usuario { get; set; }
    }
}
