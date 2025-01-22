using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface ISolPractBhCmd
    {
        public Task<IEnumerable<SolPractBhDto>> GetSolPractAsync(
               DateTime? fechaCreacionRP,
    string? startFechaRP,
    string? endFechaRP,
    string? unidad,
    string? dni,
    string? metodo,
    string? prestador,
    string? estudio,
    int? estadoPrograma,
     string? estadoTurno,
    string? usuario,
    string? servicio,
    string? obrasocial,
    string? ultimoContacto,
    string? inductor);
        public Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(
                   DateTime? fechaCreacionRP,
    string? startFechaRP,
    string? endFechaRP,
    string? unidad,
    string? dni,
    string? metodo,
    string? prestador,
    string? estudio,
    int? estadoPrograma,
     string? estadoTurno,
    string? usuario,
    string? servicio,
    string? obrasocial,
    string? ultimoContacto,
    string? inductor);
    }


}
