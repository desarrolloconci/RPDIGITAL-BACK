using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ValorModels.Dtos;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface IValorResultCmd
    {
        public Task<IEnumerable<ValoresResultDto>> GetValorResultAsync(int idPrograma, int codigoOS, int planId);
        public  Task<Decimal> GetPracticaParticularAsyncById(string codigo);
    }
}
