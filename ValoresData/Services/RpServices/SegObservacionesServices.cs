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
    public class SegObservacionesServices : ISegObservacionesServices
    {
        private readonly ISegObservacionesCmd _cmd;
        public SegObservacionesServices(ISegObservacionesCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<IEnumerable<SegObservacionesModel>> GetSegObservacionesAsync()
        {
            return await _cmd.GetSegObservacionesAsync();
        }

        public async Task<bool> ManageSegObservacionesAsyncAsync(SegObservacionesModel model)
        {
            var exist = await _cmd.GetSegObservacionesById(model);

            try
            {
                if (exist.Any())
                {
                    if (model.Metodo == "Laboratorio" || model.Metodo == "Módulo Base" || model.Metodo == "Modulo Base")
                    {

                        
                        return await _cmd.UpdateSegObservacionesVarios(model);
                    }
                    else
                        return await _cmd.UpdateSegObservacionesAsync(model);
                }
                else
                {
                    if (model.Metodo == "Laboratorio" || model.Metodo == "Módulo Base" || model.Metodo == "Modulo Base")
                    {
                        
                        return await _cmd.InsertSegObservacionesVarios(model);
                    }
                    else
                        return await _cmd.InsertSegObservacionesAsync(model);
                }
            }
            catch (Exception ex) { return false; }
        }
    }
}
