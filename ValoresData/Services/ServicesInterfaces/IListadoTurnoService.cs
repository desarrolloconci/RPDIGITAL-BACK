using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IListadoTurnoService
    {
        public Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoAsync();

        public Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha, string? idEstudio = null, string? metodo = null);
        public Task<IEnumerable<ListadoServicioTurnoDto>> GetListadoserviciosByDni(string dni, DateOnly fecha);
    }
}
