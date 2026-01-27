using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CdmRp;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
    public class SegUsuarioGestionService : ISegUsarioGestionService
    {
        private readonly ISegUsuarioGestionCmd _cmd;
        public SegUsuarioGestionService(ISegUsuarioGestionCmd cmd)
        {
            _cmd = cmd;
        }
        public async Task<bool> ManageSegUsuarioGestionAsync(SegUsuarioGestionModel model)
        {
            var exist = await _cmd.GetSegUsuarioGestion(model);
           
            try
            {
                if (exist.Any()) { 
                    if (model.Metodo == "Laboratorio"|| model.Metodo == "Módulo Base" || model.Metodo == "Modulo Base")
                     {
                     
                    return await _cmd.UpdateSegUsuarioGestionVarios(model);
                    }
                    else
                   return await _cmd.UpdateSegUsuarioGestionAsync(model);
                }
                else
                {
                    if (model.Metodo == "Laboratorio" || model.Metodo == "Módulo Base" || model.Metodo == "Modulo Base")
                    {
                        return await _cmd.InsertSegUsuarioGestionVarios(model);
                    }
                    else
                        return await _cmd.InsertSegUsuarioGestionAsync(model);
                }
            }                         
           catch (Exception ex) { return false; }
        }
    }
}
