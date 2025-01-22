using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class AsignacionInductoresModel
    {
        public int id { get; set; }
        public string DNI { get; set; }
        public string UNIDAD { get; set; }
        public int ID_INDUCTOR { get; set; }
        public string usuario { get; set; }
        public DateOnly CREADO { get; set; }
    }
}
