using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Services.RpInterfaces;
using ValorModels.Dtos.RpDto;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;

namespace ValoresData.Services.RpServices
{
  public class SegCantContactosService : ISegCantContactosService
    {
        private readonly ISegCantContactosCmd _cmd;
        public SegCantContactosService(ISegCantContactosCmd cmd)
        {
            _cmd = cmd;
        }

        public async Task<bool> DeleteSegCantContactoTotalAsync(SegCantContactosModel model)
        {  
            try
            {
                if (model.MetodoOK == "Laboratorio" || model.MetodoOK == "Módulo Base" || model.MetodoOK == "Modulo Base")
                {

                    return await _cmd.DeleteSegCantContactoTotalAsync(model);
                }
                else
                    return await _cmd.DeleteSegCantContactoUnitarioAsync(model);
            }
            catch (Exception ex) { Console.WriteLine(ex); return false; }
        }

        public async Task<bool> InsertSegCantContactosAsync(SegCantContactosModel model)
        {

            var exist = await _cmd.GetSegCantidadContactos(model);

            try
            {
                if (exist.Any())
                {
                    if (model.MetodoOK == "Laboratorio" || model.MetodoOK == "Módulo Base" || model.MetodoOK == "Modulo Base")
                    {

                        return await _cmd.UpdateSegCantidadContactosVarios(model);
                    }
                    else
                        return await _cmd.UpdateSegCantidadContactosAsync(model);
                }
                else
                {
                    if (model.MetodoOK == "Laboratorio" || model.MetodoOK == "Módulo Base" || model.MetodoOK == "Modulo Base")
                    {
                        return await _cmd.InsertSegCantContactotVarios(model);
                    }
                    else
                        return await _cmd.InsertSegCantContactosAsync(model);
                }
            }
            catch (Exception ex) { return false; }
        }
    }
}
