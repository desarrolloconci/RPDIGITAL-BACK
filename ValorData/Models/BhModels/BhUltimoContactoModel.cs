using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class BhUltimoContactoModel
    {
        public int id { get; set; }
        public int dni { get; set; }
        public string unidad { get; set; }
        public DateOnly ult_fecha { get; set; }
        public  string usuario { get; set; }
        public DateOnly creado { get; set; }
    }
}
