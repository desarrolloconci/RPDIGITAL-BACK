using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.BhDto;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhInterfaces
{
    public interface ISolPractBhService
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
    string? estadoPractica,
    string? estadoTurno,
    string? usuario,
    string? servicio,
    string? obrasocial,
    string? ultimoContacto);

        public Task<IEnumerable<SolPractBhDto>> GetSolPractAsyncDistinct(
             DateTime? fechaCreacionRP,
    string? startFechaRP,
    string? endFechaRP,
    string? unidad,
    string? dni,
    string? metodo,
    string? prestador,
    string? estudio,
    string? estadoPractica,
    string? estadoTurno,
    string? usuario,
    string? servicio,
    string? obrasocial,
    string? ultimoContacto);
    }
}
