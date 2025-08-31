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
    public class RelSegMotivoNoTurnoService : IRelSegMotivoNoTurnoService
    {
        private readonly IRelSegMotivoNoTurnoCmd _cmd;
        public RelSegMotivoNoTurnoService(IRelSegMotivoNoTurnoCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<RelSegMotivoNoTurnoModel>> GetRelSegMotivoNoTurnoAsync()
        {
            return await _cmd.GetRelSegMotivoNoTurnoAsync();
        }

        public async Task<bool> InsertRelSegMotivoNoTurnoAsync(RelSegMotivoNoTurnoModel model)
        {
            var exist = await _cmd.GetRelSegMotivoById(model);

            try
            {
                if (exist.Any())
                {
                    if (model.MetodoOK == "Laboratorio" || model.MetodoOK == "Módulo Base" || model.MetodoOK == "Modulo Base")
                    {

                        return await _cmd.UpdateRelSegMotivoNoVarios(model);
                    }
                    else
                        return await _cmd.UpdateRelSegMotivoNoAsync(model);
                }
                else
                {
                    if (model.MetodoOK == "Laboratorio" || model.MetodoOK == "Módulo Base" || model.MetodoOK == "Modulo Base")
                    {
                        return await _cmd.InsertRelSegMotivoNoVarios(model);
                    }
                    else
                        return await _cmd.InsertRelSegMotivoNoTurnoAsync(model);
                }
            }
            catch (Exception ex) { return false; }
        }
    }
}
