using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class ListadoTurnoService : IListadoTurnoService
    {
        private readonly IListadoTurnoCmd _listadoTurnoCmd;
        public ListadoTurnoService(IListadoTurnoCmd listadoTurnoCmd)
        {
            _listadoTurnoCmd = listadoTurnoCmd;
        }

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoAsync()
        {
            return await _listadoTurnoCmd.GetListadoTurnoAsync();
        }

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha, string? idEstudio = null, string? metodo = null)
        {
            return await _listadoTurnoCmd.GetListadoTurnoByDni(dni, fecha, idEstudio, metodo);

        }

        public async Task<IEnumerable<ListadoServicioTurnoDto>> GetListadoserviciosByDni(string dni, DateOnly fecha)
        {
            return await _listadoTurnoCmd.GetListadoserviciosByDni(dni, fecha);
        }
    }
}
