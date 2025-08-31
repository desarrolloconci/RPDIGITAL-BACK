using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Commands.CmdInterfaces
{
    public interface INnRelMotivoNoTurnoCmd
    {
        public Task<IEnumerable<NnRelMotivoNoTurnoModel>> GetRelMotivoNoTurnoAsync();
        public Task<bool> InsertMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel);
        public Task<bool> UpdateMotivoNoTurnoAsync(NnRelMotivoNoTurnoModel motivoNoTurnoModel);
        public Task<bool> InsertMotivoNoTurnoAsyncVarios(NnRelMotivoNoTurnoModel motivoNoTurnoModel);
        public Task<bool> DeletMotivoNoTurnoAsync(string idpedido, string metodo);
        public Task<bool> DeletMotivoNoTurnoUnitarioAsync(string idEstudio, string idPedido);
    }
}
