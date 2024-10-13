using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ValoresData.Commands.CmdValor
{
    public class TablaResumenCmd : ITablaResumenCmd
    {
        private readonly DataBaseContext _context;
        public TablaResumenCmd(DataBaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }
        public async Task<IEnumerable<TablaResumenDto>> GetTablaResumenAsync(int idPrograma, int mes, int año)
        {

            var resultados = await (from v in _context.T_AUX_TCB
                                    join e in _context.Excepciones_os_plan_cod on new { CodigoPractica = v.CodigoPractica, PlanId = v.PlanId, CodigoOS = v.codigoOS } equals new { CodigoPractica = e.Cod_practica, PlanId = e.plan_id, CodigoOS = e.os_cod } into eGroup
                                    from e in eGroup.DefaultIfEmpty()
                                    join pr in _context.Practicas on new { CodPractica = v.CodigoPractica, Nombre_Practica = v.NombreDeEstudio } equals new { CodPractica = pr.Cod_practica, Nombre_Practica = pr.Nombre_Practica }
                                    join p in _context.programas on pr.Id_Programa equals p.id
                                    join x in _context.Importe_Minimo_Programa on p.id equals x.id_programa
                                    where p.id == idPrograma && x.fecha.Month == mes && x.fecha.Year == año
                                    group new { v, pr, p, e, x } by new { v.Plan, v.codigoOS, v.Os, pr.Id_Programa, p.Nombre_programa, x.importe_minimo, x.importe_minimo_apross } into g
                                    select new
                                    {
                                        Datos = g.Key,
                                        TotalConvenio = g.Sum(x => x.v.TotalConvenio),
                                        Coseguro = g.Sum(x => x.v.Coseguro),
                                        ValoresConvenio = g.Select(x => x.v.TotalConvenio).ToList()
                                    }).ToListAsync();

            var listaDtos = resultados.Select(g => new TablaResumenDto
            {
                Plan = g.Datos.Plan.ToString(),
                codOs = g.Datos.codigoOS,
                os = g.Datos.Os.ToString(),
                programa_id = g.Datos.Id_Programa,
                Nombre_programa = g.Datos.Nombre_programa,
                TotalConvenio = g.TotalConvenio,
                Coseguro = g.Coseguro,
                importe_minimo = g.Datos.importe_minimo,
                importe_minimo_apross =  g.Datos.importe_minimo_apross,
                importe_a_pagar = (double)CalcularImporteAPagar(idPrograma, g.Datos.codigoOS, g.TotalConvenio, g.Coseguro, (decimal)g.Datos.importe_minimo,(decimal) g.Datos.importe_minimo_apross, g.ValoresConvenio)
            }).ToList();

            return listaDtos;
        }

        private decimal CalcularImporteAPagar(int idPrograma, int codigoOS, decimal totalConvenio, decimal coseguro, decimal importeMinimo, decimal importeMinimoApross, List<decimal> valoresConvenio)
        {
            bool todosMayorCero = valoresConvenio.All(v => v > 0);
             List<int> codigosOSExc = [224, 2855];
            if (idPrograma == 1023 && todosMayorCero || codigosOSExc.Contains(codigoOS) )
            {
                
                return coseguro; 
            }

            if (idPrograma == 16 && codigoOS == 1023)
            {
                return Math.Abs((importeMinimo + coseguro - totalConvenio));
            }

            if (idPrograma == 1023 && codigoOS == 1)
            {
                return importeMinimo;
            }

            if (codigoOS == 206 && idPrograma != 1023)
            {
                return (coseguro + importeMinimoApross);
            }

            if (importeMinimo >= totalConvenio)
            {
                return Math.Abs((totalConvenio - (coseguro + importeMinimo)));
            }

            return coseguro;
        }
    }
}
