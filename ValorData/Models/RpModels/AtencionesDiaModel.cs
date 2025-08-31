using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.RpModels
{
    public class AtencionesDiaModel
    {
        public int NROATENCION { get; set; }
        public DateTime FECHATENCION { get; set; }
        public int CODIGOOS { get; set; }
        public string SIGLAOS { get; set; }
        public string NOMBREOS { get; set; }
        public Int16 PLAN_ID { get; set; }
        public string NOMBREPLAN { get; set; }
        public string NRO_AFILIADO { get; set; }
        public string APELLIDOPACIENTE { get; set; }
        public string NOMBREPACIENTE { get; set; }
        public DateTime FEC_NAC { get; set; }
        public string MAIL { get; set; }
        public DateTime FECHACARGA { get; set; }
        public string TELEFONOPAC { get; set; }
        public string TELEFONOCEL { get; set; }
        public string TIPODOCPACIENTE { get; set; }
        public string NRODOCPACIENTE { get; set; }
        public Int16 SERVICIO_ID { get; set; }
        public Int16 Depto_id { get; set; }
        public string SERVICIO { get; set; }
        public int MPEFECTOR { get; set; }
        public string NOMBREEFECTOR { get; set; }
    }
}
