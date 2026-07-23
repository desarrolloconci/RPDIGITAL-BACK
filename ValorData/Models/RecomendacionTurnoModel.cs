using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorModels.Models
{
    // Sin tabla/vista propia: solo se consulta via FromSqlRaw en CompletarRecomendadoAsync
    // (ListadoTurnoCmd), para saber que turnos matchean alguna de las 3 reglas de
    // PP_BUSCAR_ATENCIONES_BH_3 contra el estudio/metodo del pedido seleccionado.
    public class RecomendacionTurnoModel
    {
        public int SERVICIO_ID { get; set; }
    }
}
