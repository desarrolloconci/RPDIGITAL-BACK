using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class InteresesTarjetasModel
    {
        public int id { get; set; }
        public string tarjeta { get; set; }
        public int cuotas { get; set; }
        public double interes { get; set; }
    }
}
