using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValorModels.Models.BhModels;

namespace ValoresData.Services.SolPractBhServices
{
    public class BhEstudiosService : IBhEstudiosService
    {
        private readonly IBhEstudiosCmd _bhEstudiosCmd;
        public BhEstudiosService(IBhEstudiosCmd bhEstudiosCmd)
        {
            _bhEstudiosCmd = bhEstudiosCmd;
        }
        public Task<IEnumerable<BhEstudiosModel>> GetBhEstudiosAsync()
        {
            return _bhEstudiosCmd.GetBhEstudiosAsync();
        }
    }
}
