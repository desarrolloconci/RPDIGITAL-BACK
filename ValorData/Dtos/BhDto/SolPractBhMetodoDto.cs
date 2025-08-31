using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Dtos.BhDto
{
    public class SolPractBhMetodoDto
    {
        public string METODOOK { get; set; }
        public string METODOOK2 { get; set; }
        public DateTime FECHA { get; set; }
        public string? ESTUDIO { get; set; }
        public DateTime? tur_fecha { get; set; }
        public int ATENDIDO { get; set; }
        public string PRESTADORQUEGENERASOLICITUD { get; set; }
        public string Estado_pedido { get; set; }
        public int? CONFESPECIAL { get; set; }
        public string idPedido { get; set; }
        public string? INDUCTOR { get; set; }
        public int? INDUCTOR_ID { get; set; }
        public int estado_Programa { get; set; }
        public string IDESTUDIO { get; set; }
        public int Estado_Turno_id { get; set; }
        public IEnumerable<SolPractBhDto> SolPractBhDtos { get; set; }
    
    }
}
