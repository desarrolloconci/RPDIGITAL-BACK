using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class ObservacionesPacientesBhModel
    {
        public int id { get; set; }
        public string dni {  get; set; }
        public string Observacion { get; set; }
        public string usuario { get; set; }
        public DateOnly creado { get; set; }
    }
}
