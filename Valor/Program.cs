using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;
using ValoresData.Commands;
using ValoresData.Commands.CdmRp;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Commands.CmdLogin.CmdLogin;
using ValoresData.Commands.CmdLogin.CmdLoginInterfaces;
using ValoresData.Commands.CmdSolPract;
using ValoresData.Commands.CmdValor;
using ValoresData.Context;
using ValoresData.Services;
using ValoresData.Services.LoginInterfaces;
using ValoresData.Services.LoginService;
using ValoresData.Services.RpInterfaces;
using ValoresData.Services.RpServices;
using ValoresData.Services.ServicesInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValoresData.Services.SolPractBhServices;
using ValorModels.Dtos;
using ValorModels.Models;
using SegCantContactosCmd = ValoresData.Commands.CdmRp.SegCantContactosCmd;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/api-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("sql");
var connectionString2 = builder.Configuration.GetConnectionString("sql2");
var connectionString3 = builder.Configuration.GetConnectionString("sql3");
var pchim = builder.Configuration.GetConnectionString("pchim");
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DataBase2Context>(options => options.UseSqlServer(connectionString2));
builder.Services.AddDbContext<DataBase3Context>(options => options.UseSqlServer(connectionString3));
builder.Services.AddDbContext<PchimContext>(options => options.UseSqlServer(pchim));
builder.Services.Configure<GmailSettingModel>(builder.Configuration.GetSection("GmailSettings"));
builder.Services.AddScoped<IValorService, ValorService>();
builder.Services.AddScoped<IValorCmd, ValorCmd>();
builder.Services.AddScoped<IPracticasService, PracticaService>();
builder.Services.AddScoped<IPracticasCmd, PracticasCmd>();
builder.Services.AddScoped<IProgramaService, ProgramasService>();
builder.Services.AddScoped<IProgramaCmd,PogramasCmd>();
builder.Services.AddScoped<IValorResultCmd, ValorResultCmd>();
builder.Services.AddScoped<IValorResutlService, ValorResultService>();
builder.Services.AddScoped<IAuxPracticasCmd, AuxPracticasCmd>();
builder.Services.AddScoped<IAuxPracticasService, AuxPracticasService>();
builder.Services.AddScoped<ITokenBuilder, TokenService>();
builder.Services.AddScoped<ILoginValidationData,LoginCmd>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IMontoMinimoProgramaCmd,MontoMinimoProgramaCmd>();
builder.Services.AddScoped<IMontoMinimoProgramaService, MontoMinimoProgramaService>();
builder.Services.AddScoped<IInteresesTarjetasCmd, InteresesTarjetasCmd>();
builder.Services.AddScoped<IInteresTarjetasServices, InteresesTarjetasService>();
builder.Services.AddScoped<IExcepcionesCmd, ExcepcionesCmd>();
builder.Services.AddScoped<IExcepcionesService, ExcepcionesService>();
builder.Services.AddScoped<IExcepcionOsPlanCodService, ExcepcionOsPlanCodService>();
builder.Services.AddScoped<IExcepcionOsPlanCodCmd, ExcepcionOsPlanCodCmd>();
builder.Services.AddScoped<ITablaResumenService, TablaResumenService>();
builder.Services.AddScoped<ITablaResumenCmd, TablaResumenCmd>();
builder.Services.AddScoped<ISolPractBhCmd, SolPractBhCmd>();
builder.Services.AddScoped<ISolPractBhService, SolPractBhService>();
builder.Services.AddScoped<IRelSolPractService, RelSolPractService>();
builder.Services.AddScoped<IReslSolPractCmd, RelSolPractCmd>();
builder.Services.AddScoped<IRelSolPractBhUtilsService, RelSolPractBhUtilsService>();
builder.Services.AddScoped<IRelSolPractBhUtilsCmd, RelSolPractBhUtilsCmd>();
builder.Services.AddScoped<IListadoTurnoCmd, ListadoTurnoCmd>();
builder.Services.AddScoped<IListadoTurnoService, ListadoTurnoService>();
builder.Services.AddScoped<IRolUserService, RolUserService>();
builder.Services.AddScoped<IRolUserCmd, RolUserCmd>();
builder.Services.AddScoped<IInstructivoService, InstructivoService>();
builder.Services.AddScoped<IInstructivoCmd, InstructivoCmd>();
builder.Services.AddTransient<ISendMailService,SendMailService>();
builder.Services.AddSingleton<IMailQueue, MailQueue>();
builder.Services.AddHostedService<MailQueueHostedService>();
builder.Services.AddScoped<IEstadoPedidoManualCmd, EstadoPedidoManualCmd>();
builder.Services.AddScoped<IEstadoPedidoManualService, EstadoPedidoManualService>();
builder.Services.AddScoped<IObservacionesPacientesBhService, ObservacionesPacientesBhService>();
builder.Services.AddScoped<IObservacionesPacientesBhCmd, ObservacionesPacintesBHCmd>();
builder.Services.AddScoped<ISolPractBhPedidoManualCmd, SolPractBhPedidoManualCmd>();
builder.Services.AddScoped<ISolPractBhPedidoManualService, SolpractBhPedidoManualService>();
builder.Services.AddScoped<IBhEstudiosService, BhEstudiosService>();
builder.Services.AddScoped<IBhEstudiosCmd, BhEstudiosCmd>();
builder.Services.AddScoped<IAsignacionInductoresService, AsignacionInductoresService>();
builder.Services.AddScoped<IAsignacionInductoresCmd, AsignacionInductoresCmd>();
builder.Services.AddScoped<IAsignacionEstadoProgramaCmd, AsignacionEstadoProgramaCmd>();
builder.Services.AddScoped<IAsignacionEstadoProgramaService, AsignacionEstadoProgramaService>();
builder.Services.AddScoped<IGrupoEstudiosService, GrupoEstudiosService>();
builder.Services.AddScoped<IGrupoEstudiosCmd, GrupoEstudiosCmd>();
builder.Services.AddScoped<IBhBateriasEstudiosService, BhBateriasEstudiosService>();
builder.Services.AddScoped<IBhBateriasEstudiosCmd, BhBateriasEstudiosCmd>();
builder.Services.AddScoped<IFichaPacienteCmd, FichaPacienteCmd>();
builder.Services.AddScoped<IFichaPacienteServicio, FichaPacienteServicio>();
builder.Services.AddScoped<IBhUltimoContactoService, UltimoContactoService>();
builder.Services.AddScoped<IBhUltimoContactoCmd, BhUltimoContactoCmd>();
builder.Services.AddScoped<IAtencionesDiaService, AtencionesDiaServices>();
builder.Services.AddScoped<IAtencionesDialCmd, AtencionesDiaCmd>();
builder.Services.AddScoped<IAtencionesPacienteService, AtencionesPacienteService>();
builder.Services.AddScoped<IAtencionesPacienteCmd, AtencionesPacienteCmd>();
builder.Services.AddScoped<IPrestadoresRpService, PrestadoresRpSerivce>();
builder.Services.AddScoped<IPrestadoresRpCmd, PrestadoresRpCmd>();
builder.Services.AddScoped<IRelEspeServiciosServices, RelEspServiciosServices>();
builder.Services.AddScoped<IRelEspServiciosCmd, RelEspServiciosCmd>();
builder.Services.AddScoped<IRelEspBateriaService, RelEspBateriaService>();
builder.Services.AddScoped<IRelEspBateriaCmd, RelEspBateriaCmd>();
builder.Services.AddScoped<INnRelMotivoNoTurnoCmd,NnRelMotivoNoturnoCmd>();
builder.Services.AddScoped<ISegMotivoNoTurnoService, SegMotivoNoturnoService>();
builder.Services.AddScoped<ISegMotivoNoTurnoCmd, SegMotivoNoTurnoCmd>();
builder.Services.AddScoped<INnRelMotivoNoTurnoService, NnRelMotivoNoTurnoService>();
builder.Services.AddScoped<IRelSegMotivoNoTurnoService, RelSegMotivoNoTurnoService>();
builder.Services.AddScoped<IRelSegMotivoNoTurnoCmd, RelSegMotivoNoTurnoCmd>();
builder.Services.AddScoped<ISegCantContactosCmd, SegCantContactosCmd>();
builder.Services.AddScoped<ISegCantContactosService, SegCantContactosService>();
builder.Services.AddScoped<ISegUsarioGestionService, SegUsuarioGestionService>();
builder.Services.AddScoped<ISegUsuarioGestionCmd, SegUsuarioGestionCmd>();
builder.Services.AddScoped<ISegGrupoGestionCmd, SegGrupoGestionCmd>();
builder.Services.AddScoped<ISegGrupoGestionService, SegGrupoGestionService>();
builder.Services.AddScoped<ISegUsuariosService, SegUsuariosService>();
builder.Services.AddScoped<ISegUsuariosCmd, SegUsuariosCmd>();
builder.Services.AddScoped<ISegMetodoService, SegMetodoService>();
builder.Services.AddScoped<ISegMetodoCmd, SegMetodoCmd>();
builder.Services.AddScoped<ISegObservacionesServices, SegObservacionesServices>();
builder.Services.AddScoped<ISegObservacionesCmd, SegObservacionesCmd>();
builder.Services.AddScoped<IDatosPacientesCargaManualService, DatosPacientesCargaManualService>();
builder.Services.AddScoped<IDatosPacientesCargaManualCmd, DatosPacientesCargaManualCmd>();
builder.Services.AddScoped<IUsuariosCmd, UsuariosCmd>();
builder.Services.AddScoped<IUsusarioService, UsuariosService>();
builder.Services.AddScoped<ILogsService, LogsService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.WithOrigins("http://localhost:3001",
            "http://localhost:3000",
            "http://localhost:3002",
            "http://localhost:3003",
            "http://localhost:3004",
            "http://localhost:3005",
            "http://192.168.9.211:3000",
            "http://192.168.9.211:3001",
            "http://192.168.9.211:85",
            "http://192.168.9.211",
            "http://192.168.9.210:3000",
            "http://192.168.9.210:85",
            "http://192.168.9.5:3000",
            "http://192.168.9.5:85",
            "http://192.168.9.5:3007",
            "http://192.168.9.5:3013",
            "http://192.168.9.2:3005",
            "http://localhost:85")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath}{QueryString} respondió {StatusCode} en {Elapsed:0.0000} ms";
    options.GetLevel = (httpContext, elapsedMs, ex) => ex != null
        ? LogEventLevel.Error
        : elapsedMs > 3000
            ? LogEventLevel.Warning
            : LogEventLevel.Information;
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("QueryString", httpContext.Request.QueryString.Value);
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("NuevaPolitica");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://0.0.0.0:7174");

}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación no pudo iniciar correctamente");
}
finally
{
    Log.CloseAndFlush();
}
