using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    // Sin tabla/vista propia: solo se consulta via FromSqlRaw (AtencionesPacienteCmd), contra
    // MOVENCA/MOVPRAC/Nomenclador/SERVICIOS en Geclisa (misma fuente que usa /rp/turnos
    // para "atenciones del dia" via vMultiConsultaNatanet).
    public class AtencionPacienteModel
    {
        public int NroAtencion { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public string? Practica { get; set; }
        public string? CodigoPractica { get; set; }
        public string? Servicio { get; set; }
    }
}
