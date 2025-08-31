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
    public class SegMotivoNoturnoService : ISegMotivoNoTurnoService
    {
        private readonly ISegMotivoNoTurnoCmd _cmd;
        public SegMotivoNoturnoService(ISegMotivoNoTurnoCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<SegMotivoNoTurnoModel>> GetSegMotivoNoTurnoAsync()
        {
            return await _cmd.GetSegMotivoNoTurnoAsync();
        }
    }
}
