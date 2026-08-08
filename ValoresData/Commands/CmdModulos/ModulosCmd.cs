using System.Data;
using Dapper;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Dtos.ModulosDto;

namespace ValoresData.Commands.CmdModulos
{
    // Epic 6: detalle de modulo/prestacion. pa_AteAmb_MovPraMovPreDet - sin cambios en la
    // llamada. En la traza original este SP se llamaba 2 veces seguidas con el mismo Me_id
    // (bug de doble disparo del frontend, ~5.3ms extra) - acá se expone una sola vez por
    // llamada; que no se dispare 2 veces por click es responsabilidad del cliente nuevo.
    public class ModulosCmd : IModulosCmd
    {
        private readonly IGeclisaConnectionFactory _connectionFactory;

        public ModulosCmd(IGeclisaConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ModuloDetalleDto>> GetDetalleModuloAsync(int meId)
        {
            using var conn = _connectionFactory.CreateConnection();
            return await conn.QueryAsync<ModuloDetalleDto>(
                "pa_AteAmb_MovPraMovPreDet",
                new { Me_id = meId },
                commandType: CommandType.StoredProcedure);
        }
    }
}
