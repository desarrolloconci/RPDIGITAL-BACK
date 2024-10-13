using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class ExcepcionesModel
    {
        public int id { get; set; }
        public int codigo_os { get; set; }
        public int ID_PROGRAMA { get; set; }
        public string excepcion { get; set; }
        public DateOnly fecha { get; set; }
        public string usuario_creacion { get; set;}
    }
}
