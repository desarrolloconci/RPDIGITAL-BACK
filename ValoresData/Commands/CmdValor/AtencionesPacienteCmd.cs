using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;

namespace ValoresData.Commands.CmdValor
{
    public class AtencionesPacienteCmd : IAtencionesPacienteCmd
    {
        private readonly DataBase2Context _dbContext;
        public AtencionesPacienteCmd(DataBase2Context dbContext)
        {
            _dbContext = dbContext;
        }

        // Atenciones reales del paciente de los ultimos N dias, con la practica realizada y el
        // servicio donde se atendio. Misma fuente que usa /rp/turnos para "atenciones del dia"
        // (MOVENCA/MOVPRAC en Geclisa, sin pasar por el servidor enlazado a MIND): vMultiConsultaNatanet
        // no expone la practica, asi que se agrega el join a MOVPRAC + Nomenclador para resolverla.
        public async Task<IEnumerable<AtencionPacienteModel>> GetAtencionesPacienteAsync(string dni, int dias = 60)
        {
            string dniSinCeros = dni.TrimStart('0');
            string dniFormateado = dniSinCeros.PadLeft(9, '0');

            var pDni = new SqlParameter("@dni", dniFormateado);
            var pDias = new SqlParameter("@dias", dias);

            var sql = @"
                SELECT ME.ME_ID AS NroAtencion, ME.ME_FECHA AS FechaAtencion,
                       N.nom_nom AS Practica, MP.nom_cod AS CodigoPractica,
                       LTRIM(RTRIM(SER.SERV_NOMBRE)) AS Servicio
                FROM MOVENCA ME
                INNER JOIN MOVPRAC MP ON ME.ME_ID = MP.ME_ID
                LEFT JOIN Nomenclador N ON MP.nom_id = N.nom_id AND MP.nom_cod = N.nom_cod
                LEFT JOIN SERVICIOS SER ON MP.Serv_id = SER.SERV_ID
                WHERE ME.ME_NRODOC = @dni
                  AND ME.ME_FECHA >= DATEADD(DAY, -@dias, CAST(GETDATE() AS date))
                ORDER BY ME.ME_FECHA DESC";

            return await _dbContext.AtencionesPaciente.FromSqlRaw(sql, pDni, pDias).ToListAsync();
        }
    }
}
