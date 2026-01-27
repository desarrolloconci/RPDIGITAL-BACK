using Microsoft.EntityFrameworkCore.Metadata;
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
    public class SegMetodoService : ISegMetodoService
    {
        private readonly ISegMetodoCmd _cmd;
        public SegMetodoService(ISegMetodoCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<SegMetodoModel>> GetMetodosAsync()
        {
            return await _cmd.GetMetodosAsync();
        }
    }
}
