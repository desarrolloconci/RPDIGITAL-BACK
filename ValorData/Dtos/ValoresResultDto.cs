using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos
{
    public class ValoresResultDto
    {
        public string CodigoPractica { get; set; }
        public string NombrePractica { get; set; }
        public decimal TotalConvenio { get; set; }
        public string? OtroTipoDeAutorizacion { get; set; }
        public string Derivacion { get; set; }
        public decimal Coseguro { get; set; }
        public decimal ValorParticular { get; set; }
        public string excepcion { get; set; }
    }
}
