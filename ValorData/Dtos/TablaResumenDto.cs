using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos
{
    public class TablaResumenDto
    {
        public int codOs { get; set; }
        public string os { get; set; }
        public string Plan { get; set; }
        public int programa_id {get; set; }
        public string Nombre_programa { get; set; }
        public decimal TotalConvenio { get; set; }
        public decimal Coseguro { get; set; }
        public double importe_minimo { get; set; }
        public double importe_minimo_apross { get; set; }
        public double importe_a_pagar { get; set; }
    }
}
