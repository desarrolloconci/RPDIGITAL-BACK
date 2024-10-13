using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class ExcepcionOsPlanCodModel
    {
        public int id { get; set; }
        public int os_cod { get; set; }
        public short plan_id { get; set; }
        public string Cod_practica { get; set; }
        public string Nombre_Practica { get; set; }
        public string excepcion { get; set; }
        public DateOnly Creado { get; set; }
        public string Usuario { get; set; }

    }
}
