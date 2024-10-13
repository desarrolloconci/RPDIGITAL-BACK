using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ValoresData.Commands.CmdInterfaces;
using ValoresData.Commands.CmdLogin.CmdLogin;
using ValoresData.Commands.CmdLogin.CmdLoginInterfaces;
using ValoresData.Commands.CmdSolPract;
using ValoresData.Commands.CmdValor;
using ValoresData.Context;
using ValoresData.Services;
using ValoresData.Services.LoginInterfaces;
using ValoresData.Services.LoginService;
using ValoresData.Services.ServicesInterfaces;
using ValoresData.Services.SolPractBhInterfaces;
using ValoresData.Services.SolPractBhServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("sql");
var connectionString2 = builder.Configuration.GetConnectionString("sql2");
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DataBase2Context>(options => options.UseSqlServer(connectionString2));
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
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

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
