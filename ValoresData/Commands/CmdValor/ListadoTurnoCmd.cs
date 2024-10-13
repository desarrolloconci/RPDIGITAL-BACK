using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Context;
using ValorModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace ValoresData.Commands.CmdValor
{
    public class ListadoTurnoCmd : IListadoTurnoCmd
    {
        private readonly DataBase2Context _dbContext;

        public ListadoTurnoCmd(DataBase2Context dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoAsync()
        {
            return await _dbContext.vListadoTurnos.Take(100).ToListAsync();
        }

        public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha)
        {
            var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
            var result = await _dbContext.vListadoTurnos.Where(e => e.fic_nrodoc == dni && e.tur_fecha.Value > fechaDateTime && e.cancelado == "NO").ToListAsync();
            return result;
        }
        /* private readonly DataBase2Context _dbContext;
         private readonly IConfiguration _configuration;

         public ListadoTurnoCmd(DataBase2Context dbContext, IConfiguration configuration)
         {
             _dbContext = dbContext;
             _configuration = configuration;
         }

         [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
         public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword,
                                             int dwLogonType, int dwLogonProvider, out IntPtr phToken);

         [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
         public extern static bool DuplicateToken(IntPtr hToken, int impersonationLevel, out IntPtr hNewToken);

         [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
         public extern static bool CloseHandle(IntPtr handle);

         private void RunWithImpersonation(Action action)
         {
             var username = _configuration["Impersonation:Username"];
             var domain = _configuration["Impersonation:Domain"];
             var password = _configuration["Impersonation:Password"];

             if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(password))
             {
                 throw new InvalidOperationException("Credenciales de impersonación no están definidas.");
             }

             IntPtr userToken = IntPtr.Zero;
             IntPtr dupToken = IntPtr.Zero;

             try
             {
                 if (LogonUser(username, domain, password, 4, 0, out userToken)) // Probar con 4 (LOGON32_LOGON_BATCH) o otros tipos
                 {
                     if (DuplicateToken(userToken, 2, out dupToken)) // Mantener 2 para impersonación
                     {
                         using (WindowsIdentity identity = new(dupToken))
                         {
                             WindowsIdentity.RunImpersonated(identity.AccessToken, () =>
                             {
                                 action();  // Ejecutar la acción bajo el contexto del usuario impersonado
                             });
                         }
                     }
                     else
                     {
                         int errorCode = Marshal.GetLastWin32Error();
                         throw new UnauthorizedAccessException($"Error al duplicar el token. Código de error: {errorCode}");
                     }
                 }
                 else
                 {
                     int errorCode = Marshal.GetLastWin32Error();
                     throw new UnauthorizedAccessException($"Error al loguear el usuario. Código de error: {errorCode}");
                 }
             }
             finally
             {
                 if (userToken != IntPtr.Zero)
                     CloseHandle(userToken);
                 if (dupToken != IntPtr.Zero)
                     CloseHandle(dupToken);
             }
         }
             public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoAsync()
         {
             IEnumerable<ListadoTurnosModel> result = null;

             // Ejecuta la consulta bajo el contexto de impersonación
             await Task.Run(() =>
             {
                 RunWithImpersonation(() =>
                 {
                     result = _dbContext.vListadoTurnos.Take(100).ToList();
                 });
             });

             return result;
         }

         public async Task<IEnumerable<ListadoTurnosModel>> GetListadoTurnoByDni(string dni, DateOnly fecha)
         {
             IEnumerable<ListadoTurnosModel> result = null;

             // Ejecuta la consulta bajo el contexto de impersonación
             await Task.Run(() =>
             {
                 RunWithImpersonation(() =>
                 {
                     var fechaDateTime = fecha.ToDateTime(TimeOnly.MinValue);
                     result = _dbContext.vListadoTurnos
                                 .Where(e => e.fic_nrodoc == dni && e.tur_fecha.Value > fechaDateTime && e.cancelado == "NO")
                                 .ToList();
                 });
             });

             return result;
         }
     }*/
    }
}