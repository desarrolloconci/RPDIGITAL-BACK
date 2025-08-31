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
    public class PrestadoresRpSerivce: IPrestadoresRpService
    {
        private readonly IPrestadoresRpCmd _prestadoresRp;
        public PrestadoresRpSerivce(IPrestadoresRpCmd prestadoresRp)
        {
            _prestadoresRp = prestadoresRp;
        }

        public async Task<IEnumerable<PrestadoresRpModel>> GePrestadoresRpAsync()
        {
           return await  _prestadoresRp.GePrestadoresRpAsync();
        }
    }
}
