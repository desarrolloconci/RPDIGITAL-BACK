using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    public class ValorModel
    {
        public int ID { get; set; }
        [Column("TIPO DE ESTUDIO")]
        public string? TipoDeEstudio { get; set; }
        [Column("NOMBRE DE ESTUDIO")]
        public string? NombreDeEstudio { get; set; }
        [Column("ID NOMBRE DE ESTUDIO")]
        public string? IdNombreDeEstudio { get; set; }
        [Column("CODIGO DE PRACTICA")]
        public string? CodigoDePractica { get; set; }
        [Column("DURACION (turnos)")]
        public int Duracion { get; set; }
        // public int Id_Servicio { get; set; }
        [Column("VISIBLE TURNERO WEB")]
        public string? VisibleTurneroWeb { get; set; }
        [Column("Nomenclador Id")]
        public short NomencladorId { get; set; }
        public string? Nomenclador { get; set; }
        public string? Origen { get; set; }
        [Column("Código Práctica")]
        public string? CodigoPractica { get; set; }
        [Column("Práctica")]
        public string? Practica { get; set; }
        [Column("Código OS")]
        public int codigoOS { get; set; }
        [Column("os_id")]
        public short OsId { get; set; }
        public string? Os { get; set; }
        [Column("Vigencia Presc.")]
        public short VigenciaPresc { get; set; }
        [Column("plan_id")]
        public short PlanId { get; set; }
        public string? Plan { get; set; }
        [Column("Total Convenio")]
        public decimal TotalConvenio { get; set; }
        public DateOnly? Fecha { get; set; }
        public string? Bono { get; set; }
        [Column("Derivación")]
        public string? Derivacion { get; set; }
        public short? Autoriza { get; set; }
        [Column("Otro Tipo de Autorización")]
        public string? OtroTipoDeAutorizacion { get; set; }
        [Column("Autorización")]
        public string? Autorizacion { get; set; }
        public decimal Coseguro { get; set; }
        public string? Modo { get; set; }
        public string? Traductor { get; set; }
    }
}

