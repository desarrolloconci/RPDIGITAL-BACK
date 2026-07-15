using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models.BhModels
{
    public class RelSolPractModel
    {
        public int id { get; set; }
        public string? idPedido { get; set; }
        public string? idEstudio { get; set; }
        public string? inductor { get; set; }
        public string? estadoPrograma { get; set; }
        public string? estadoTurno { get; set; }
        public DateOnly fechaGestion { get; set; }
        public string? observaciones { get; set; }
        public DateOnly creado { get; set; }
        public string? usuario { get; set; }
        public int ? turno_id { get; set; }
        public int? IDESTUDIO_NUM { get; set; }
        public string? metodoOK { get; set; }
        public DateTime tur_fecha { get; set; }
        public TimeOnly hs_Ini { get; set; }
        public string? serv_nombre { get; set; }
        public int? servicio_id { get; set; }
    }
}
