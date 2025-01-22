using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Commands.CmdValor;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class InstructivoService : IInstructivoService
    {
        private readonly IInstructivoCmd _instructivoCmd;
        public InstructivoService(IInstructivoCmd instructivoCmd)
        {
            _instructivoCmd = instructivoCmd;
        }

        public async Task<IEnumerable<InstructivoModel>> GetInstructivoAsync()
        {
            return await _instructivoCmd.GetInstructivoAsync();
        }

        public async Task<InstructivoModel> GetInstructivoByCodnomAsync(int id)
        {
            return await _instructivoCmd.GetInstructivoByCodnomAsync(id);
        }
    }
}
