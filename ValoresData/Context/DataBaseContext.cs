using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Models;
using ValorModels.Models.BhModels;
using ValorModels.Models.UsersModels;

namespace ValoresData.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<ValorModel> T_AUX_TCB { get; set; }
        public DbSet<PracticasModel> Practicas { get; set; }
        public DbSet<ProgramasModel> programas { get; set; }
        public DbSet<OsPlanModel> OSPlanConCodOs { get; set; }
        public DbSet<AuxPracticasModel> V_AUX_PRACTICAS { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<MontoMinimoProgramaModel> Importe_Minimo_Programa { get; set; }
        public DbSet<ExcepcionesModel> excepciones { get; set; }
        public DbSet<InteresesTarjetasModel> Intereses_tarjetas { get; set; }
        public DbSet<ExcepcionOsPlanCodModel> Excepciones_os_plan_cod { get; set; }
        public DbSet<RelSolPractModel> REL_SOL_PRACT { get; set; }
        public DbSet<SolPractBhModel> V_BEALTH_SOLPRAC { get; set; }
        public DbSet<RelSolPractBhMetodoModel> V_SolPractBhMetodo { get; set; }
        public DbSet<RelSolPractBhUnidadModel> V_SolPractBhUnidad { get; set; }
        public DbSet<RelSolPractBhServicioSolModel> V_SolPractBhEspecialidad { get; set; }
        public DbSet<RelSolPractBhOsModel> V_SolPractBhOs { get; set; }
        public DbSet<RelSolPractBhInductoresModel> V_SolPractBhInductores { get; set; }
        public DbSet<RelSolPractBhEstadoProgramaModel> Bh_Estado_Programa { get; set; }
        public DbSet<RelSolPractBhEstadoTurnoModel> Bh_Estado_Turno { get; set; }
        public DbSet<RolUserModel> RolUsers { get; set; }
        public DbSet<EstadoPedidoManualModel> ESTADO_PEDIDO_MANUAL { get; set; }
        public DbSet<ObservacionesPacientesBhModel> OBSERVACIONES_PACIENTES_BH { get; set; }
        public DbSet<SolPractBhPedidoManualModel> BEALTH_SOLPRACT_P_MANUAL { get; set; }
        public DbSet<BhEstudiosModel> V_BH_ESTUDIOS { get; set; }
        public DbSet<AsignacionInductoresModel> ASIGNACION_INDUCTORES { get; set; }
        public DbSet<AsignacionEstadoProgramaModel> ASIGNACION_ESTADO_PROGRAMA { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelSolPractBhMetodoModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractBhUnidadModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractBhServicioSolModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractBhOsModel>().HasNoKey();
            modelBuilder.Entity<BhEstudiosModel>().HasNoKey();
        }

    }

    public class DataBase2Context : DbContext
    {
        public DataBase2Context(DbContextOptions<DataBase2Context> options) : base(options)

        {


        }

        public DbSet<ListadoTurnosModel> vListadoTurnos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListadoTurnosModel>()
                .HasNoKey();


        }
    }
    public class DataBase3Context : DbContext
    {
        public DataBase3Context(DbContextOptions<DataBase3Context> options) : base(options)
        {
        }
        public DbSet<InstructivoModel> V_BH_DESCRIPCIONES { get; set; }

    }
}
