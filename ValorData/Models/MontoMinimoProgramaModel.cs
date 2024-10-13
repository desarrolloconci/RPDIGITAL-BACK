using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class MontoMinimoProgramaModel
    {
        public int id { get; set; }
        public int id_programa { get; set; }
        public double importe_minimo { get; set; }
        public double importe_minimo_apross { get; set; }
        public DateOnly fecha { get; set; }
        public string usuario_creacion { get; set; }
        public DateOnly creado { get; set; }

    }
}
