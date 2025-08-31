using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
    public class FichaPacienteServicio:IFichaPacienteServicio
    {
        private readonly IFichaPacienteCmd _ficha;

        public FichaPacienteServicio(IFichaPacienteCmd ficha)
        {
            _ficha = ficha;
        }
        public async Task<IEnumerable<FichaPacienteModel>> GetFichaPacienteAsync()
        {
            return await _ficha.GetFichaPacienteAsync();
        }
        public async Task<FichaPacienteModel> GetFichaPacienteDetailAsync(int dni)
        {
            return await _ficha.GetFichaPacienteDetailAsync(dni);
        }
    }
}
