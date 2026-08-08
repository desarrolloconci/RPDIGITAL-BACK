using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ValoresData.Context
{
    // Conexion cruda (Dapper) a Geclisa, separada de DataBase2Context (EF).
    // Cada llamada crea su propia SqlConnection para poder correr en paralelo
    // (un DbContext/conexion de EF no soporta multiples operaciones concurrentes).
    public interface IGeclisaConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class GeclisaConnectionFactory : IGeclisaConnectionFactory
    {
        private readonly string _connectionString;

        public GeclisaConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("sql2")
                ?? throw new InvalidOperationException("Falta la connection string 'sql2' (Geclisa) en appsettings.json");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
