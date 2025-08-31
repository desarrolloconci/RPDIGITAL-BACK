using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
    public class RelEspBateriasModel
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int bateria_id { get; set; }
        public string nombre { get; set; }
    }
}
