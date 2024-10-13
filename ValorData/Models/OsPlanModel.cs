using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class OsPlanModel
    {
        public int ID { get; set; }
        [Column("Código OS")]
        public int codigoOS { get; set; }
        public string Os { get; set; }
        [Column("plan_id")]
        public short PlanId { get; set; }
        public string Plan { get; set; }
        public string codOs { get; set; }

    }
}
