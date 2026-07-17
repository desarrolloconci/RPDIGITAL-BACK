using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    // Sin tabla/vista propia: solo se consulta via FromSqlRaw contra el servidor
    // enlazado SRV-DESA01 (TWCC.dbo.V_MT_NOMENCLADOR), para traer código y nombre
    // de práctica a partir del ESTUDIO_ID de AG_TURNO_ESTUDIO.
    public class NomencladorPracticaModel
    {
        public int ESTUDIO_ID { get; set; }
        public int NOMENCLADOR_ID { get; set; }
        public string? NOMBRE { get; set; }
        public string? PRACTICA_CODIGO { get; set; }
    }
}
