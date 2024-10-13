using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos;
using ValorModels.Dtos.LoginDto;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class ValorResultCmd : IValorResultCmd
    {
        private readonly DataBaseContext _context;
        public ValorResultCmd(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ValoresResultDto>> GetValorResultAsync(int idPrograma, int codigoOS, int planId)
        {

            var resultados = await (from v in _context.T_AUX_TCB

                                    join e in _context.Excepciones_os_plan_cod on new { CodigoPractica = v.CodigoPractica, PlanId = v.PlanId, CodigoOS = v.codigoOS } equals new { CodigoPractica = e.Cod_practica, PlanId = e.plan_id, CodigoOS = e.os_cod } into eGroup

                                    from e in eGroup.DefaultIfEmpty()
                                    join pr in _context.Practicas on new { CodPractica = v.CodigoPractica, Nombre_Practica = v.NombreDeEstudio } equals new { CodPractica = pr.Cod_practica, Nombre_Practica= pr.Nombre_Practica }
                                    join p in _context.programas on pr.Id_Programa equals p.id
                                    where p.id == idPrograma && v.codigoOS == codigoOS && v.PlanId == planId
                                    select new ValoresResultDto
                                    {
                                        CodigoPractica = v.CodigoPractica,
                                        NombrePractica = v.NombreDeEstudio,
                                        TotalConvenio = v.TotalConvenio,
                                        OtroTipoDeAutorizacion = v.Autorizacion,
                                        Derivacion = v.Derivacion,
                                        Coseguro = v.Coseguro,
                                        excepcion = e.excepcion
                                    }).ToListAsync();



            return (IEnumerable<ValoresResultDto>)resultados;
        }

        public async Task<Decimal> GetPracticaParticularAsyncById(string codigo)
        {
            var result = await (from v in _context.T_AUX_TCB
                                where v.codigoOS==1 && v.PlanId==103 && v.CodigoPractica == codigo
                                select new ValoresResultDto
                                {
                                  ValorParticular= v.TotalConvenio
                                }).FirstOrDefaultAsync();

            return result?.ValorParticular ?? 0;

        }


    }
}
