using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CdmRp
{
    public class AtencionesDiaCmd : IAtencionesDialCmd
    {
        private readonly DataBase2Context _dbContext;
        private readonly DataBaseContext _context;
        public AtencionesDiaCmd(DataBase2Context base2Context, DataBaseContext baseContext)
        {
            _context = baseContext;
            _dbContext = base2Context;
        }



        public async Task<IEnumerable<AtencionesDiaModel>> GetAtencionesDiaAsync(int UserID)
        {

            var matriculas = await GetMatriculasRelacionadasAsync(UserID);
            var matriculasLista = matriculas.Select(m => m.matricula).ToList();
            var fechaHoy = DateTime.Today;

            var atenciones = await _dbContext.vMultiConsultaNatanet
                .Where(e => matriculasLista.Contains(e.MPEFECTOR) && e.FECHATENCION.Date == fechaHoy)
                .GroupBy(e => e.NROATENCION)
                .Select(g => g.First()) 
                .ToListAsync();

            return atenciones;
        }
        public async Task<IEnumerable<AtencionesDiaModel>> GetAtencionesDiaServicioAsync(int UserID, string servicio)
        {
            var zonaArgentina = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            var ahoraArgentina = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaArgentina);
            var matriculas = await GetMatriculasRelacionadasAsync(UserID);
            var matriculasLista = matriculas.Select(m => m.matricula).ToList();
            var fechaHoy = ahoraArgentina.Date;
            var fechaMañana = fechaHoy.AddDays(1);
            if (servicio == "vdee22sut")
            {
                var atencionesCMPreconsulta = await _dbContext.vMultiConsultaNatanet
                                .Where(e => matriculasLista.Contains(e.MPEFECTOR) && e.FECHATENCION >= fechaHoy && e.FECHATENCION < fechaMañana && e.Depto_id == 54)
                                .GroupBy(e => e.NROATENCION)
                                .Select(g => g.First())
                                .ToListAsync();
                return atencionesCMPreconsulta;
            }
            if (servicio == "vdee23sut")
            {
                var circuitosMedicos = await _dbContext.vMultiConsultaNatanet
                .Where(e => e.MPEFECTOR == 999994 && e.FECHATENCION >= fechaHoy && e.FECHATENCION < fechaMañana)
                .GroupBy(e => e.NROATENCION)
                .Select(g => g.First())
                .ToListAsync();
                return circuitosMedicos;
            }

            var atenciones = await _dbContext.vMultiConsultaNatanet
                .Where(e => matriculasLista.Contains(e.MPEFECTOR) && e.FECHATENCION >= fechaHoy && e.FECHATENCION < fechaMañana && e.Depto_id != 54)
                .GroupBy(e => e.NROATENCION)
                .Select(g => g.First())
                .ToListAsync();

            return atenciones;
        }

        public async Task<IEnumerable<ListadoServicioAtencionDto>> GetServicioAtencionesDiaAsync(int UserID)
        {
            var matriculas = await GetMatriculasRelacionadasAsync(UserID);
            var matriculasLista = matriculas.Select(m => m.matricula).ToList();


            var fechaHoy = DateTime.Today;


            var atenciones = await _dbContext.vMultiConsultaNatanet
                 .Where(e =>
                     matriculasLista.Contains(e.MPEFECTOR) &&
                     e.FECHATENCION.Date == fechaHoy)
                 .GroupBy(e => e.SERVICIO)
                 .Select(g => new ListadoServicioAtencionDto
                 {
                     SERVICIO_ID = g.FirstOrDefault().SERVICIO_ID,
                     SERVICIO = g.Key.Trim()
                 })
                 .ToListAsync();

            return atenciones;
        }

        public async Task<IEnumerable<RelEspMatriculasModel>> GetMatriculasRelacionadasAsync(int UserID)
        {
            return await _context.Rel_esp_matriculas.Where(e => e.usuario_id == UserID).ToListAsync();
        }
    }
}
