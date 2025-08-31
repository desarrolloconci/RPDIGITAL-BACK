using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
  public interface IRelSegMotivoNoTurnoCmd
    {
        public Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoNoTurnoAsync();

        public Task<bool> InsertRelSegMotivoNoTurnoAsync(RelSegMotivoNoTurnoModel model);
        public Task<bool> InsertRelSegMotivoNoVarios(RelSegMotivoNoTurnoModel model);
        public Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoById(RelSegMotivoNoTurnoModel model);
        public Task<bool> UpdateRelSegMotivoNoAsync(RelSegMotivoNoTurnoModel model);
        public Task<bool> UpdateRelSegMotivoNoVarios(RelSegMotivoNoTurnoModel model);
    }
}
