using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class ListadoTurnosModel
    {
        public int? turno_id { get; set; }
        public string? fic_nrodoc { get; set; }
        public string? apellido { get; set; }
        public string? nombre { get; set; }
        public DateTime? tur_fecha { get; set; }
        public TimeOnly? Hs_Ini { get; set; }
        public string? pre_nombre { get; set; }
        public int confirmado { get; set; }
        public string? usuario_confirma { get; set; }
        public DateTime? usuario_confirma_fecha { get; set; }
        public string? tur_obs { get; set; }
        public int servicio_id { get; set; }
        public string Serv_nombre { get; set; }
        public string nom_cod { get; set; }
        public string nom_nom { get; set; }
        public string cancelado { get; set; }
        public Int16 tipoNomenclador_id { get; set; }

        // Código y nombre de práctica resueltos via AG_TURNO_ESTUDIO + V_MT_NOMENCLADOR
        // (ver CompletarPracticaAsync en ListadoTurnoCmd); si el turno no tiene estudio
        // asociado en AG_TURNO_ESTUDIO, queda null y el front usa el fallback nom_cod/nom_nom.
        [NotMapped]
        public string? Practica { get; set; }

    }
}
