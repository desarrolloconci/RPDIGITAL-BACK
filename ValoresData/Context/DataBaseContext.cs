using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorModels.Dtos.BhDto;
using ValorModels.Models;
using ValorModels.Models.BhModels;
using ValorModels.Models.RpModels;
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
        public DbSet<UserModel> BH_USERS { get; set; }
        public DbSet<MontoMinimoProgramaModel> Importe_Minimo_Programa { get; set; }
        public DbSet<ExcepcionesModel> excepciones { get; set; }
        public DbSet<InteresesTarjetasModel> Intereses_tarjetas { get; set; }
        public DbSet<ExcepcionOsPlanCodModel> Excepciones_os_plan_cod { get; set; }
        public DbSet<RelSolPractModel> REL_SOL_PRACT { get; set; }
        public DbSet<SolPractBhModel> V_BEALTH_SOLPRAC { get; set; }
        // Sin tabla/vista propia: solo se consulta via FromSqlRaw (GetSolPractRpAsync), para que
        // la materialización use el shaper compilado de EF en vez del mecanismo generico de SqlQueryRaw.
        public DbSet<SolPractBhDto> SolPractBhRpQuery { get; set; }
        public DbSet<RelSolPractBhMetodoModel> V_SolPractBhMetodo { get; set; }
        public DbSet<CacheSolPractBhMetodoModel> CACHE_SolPractBhMetodo { get; set; }
        public DbSet<RelSolPractBhUnidadModel> V_SolPractBhUnidad { get; set; }
        public DbSet<RelSolPractBhServicioSolModel> V_SolPractBhEspecialidad { get; set; }
        public DbSet<RelSolPractBhOsModel> V_SolPractBhOs { get; set; }
        public DbSet<RelSolPractBhInductoresModel> V_SolPractBhInductores { get; set; }
        public DbSet<RelSolPractBhEstadoProgramaModel> Bh_Estado_Programa { get; set; }
        public DbSet<RelSolPractBhEstadoTurnoModel> Bh_Estado_Turno { get; set; }
        public DbSet<RolUserModel> RolUsers { get; set; }
        public DbSet<EstadoPedidoManualModel> ESTADO_PEDIDO_MANUAL { get; set; }
        public DbSet<ObservacionesPacientesBhModel> OBSERVACIONES_PACIENTES_BH { get; set; }
       // public DbSet<SolPractBhPedidoManualModel> BEALTH_SOLPRACT_P_MANUAL { get; set; }
        public DbSet<SolPractBhPedidoManualModel> BEALTH_SOLPRACT_P_MANUAL_OK { get; set; }
        public DbSet<RpBorradoModel> RP_BORRADOS { get; set; }
        public DbSet<MotivoBorradoPedidoModel> MOTIVO_BORRADO_PEDIDO { get; set; }

        public DbSet<BhEstudiosModel> V_BH_ESTUDIOS { get; set; }
        public DbSet<AsignacionInductoresModel> ASIGNACION_INDUCTORES { get; set; }
        public DbSet<AsignacionEstadoProgramaModel> ASIGNACION_ESTADO_PROGRAMA { get; set; }
        public DbSet<BhBateriasEstudiosModel> V_BH_BATERIAS_ESTUDIOS { get; set; }
        public DbSet<BhBateriasModel> v_BH_BATERIAS { get; set; }
        public DbSet<RelBateriasEspModel> Rel_esp_baterias { get; set; }
        public DbSet<FichaPacienteModel> FICHA_PACIENTES_BH { get; set; }
        public DbSet<BhUltimoContactoModel> BH_ULTIMO_CONTACTO { get; set; }
        public DbSet<RelEspServiciosModel> V_Rel_esp_servicios { get; set; }
        public DbSet<RelEspServicioModel> REL_ESP_SERVICIOS { get; set; }
        public DbSet<RelEspMatriculasModel> Rel_esp_matriculas { get; set; }
        public DbSet<PrestadoresRpModel> V_PRESTADORES_RP { get; set; }
        public DbSet<RelEspBateriasModel> V_Rel_esp_baterias { get; set; }
        public DbSet<NnMotivoNoTurnoModel> NN_MOTIVO_NO_TURNO { get; set; }
        public DbSet<NnRelMotivoNoTurnoModel> NN_REL_MOTIVO_NO_TURNO { get; set; }
        public DbSet<UnionSolPractModel> V_UNION_BEHEALTH_SOLPRACT { get; set; }
        public DbSet<SolPractModel> BEALTH_SOLPRACT { get; set; }
        public DbSet<InstructivoModel> V_BH_DESCRIPCIONES { get; set; }
        public DbSet<GrupoEstudiosModel> GRUPOESTUDIOS { get; set; }
        public DbSet<GrupoEstudiosDetModel> GRUPOESTUDIOSDET { get; set; }
        public DbSet<GrupoEstudiosCreadorModel> GRUPOESTUDIOS_CREADOR { get; set; }
        public DbSet<BhBateriasPublicasModel> V_BATERIAS_UNIVERSALES { get; set; }
        public DbSet<SegMotivoNoTurnoModel> SEG_MOTIVO_NO_TURNO { get; set; }
        public DbSet<RelSegMotivoNoTurnoModel> SEG_REL_MOTIVO_NO_TURNO { get; set; }
        public DbSet<SegCantContactosModel> SEG_CANT_CONTACTOS { get; set; }
        public DbSet<SegUsuarioGestionModel> SEG_USUARIO_GESTION { get; set; }
        public DbSet<SegGrupoGestionModel> SEG_GRUPO_GESTION { get; set; }
        public DbSet<SegUsuariosModel> V_Seg_Usuarios { get; set; }
        public DbSet<SegMetodoModel> V_SEG_METODO { get; set; }
        public DbSet<SegObservacionesModel> SEG_OBSERVACIONES { get; set; }
        public DbSet<LogCambioRpModel> SEG_LOG_CAMBIOS_RP { get; set; }
        public DbSet<TicketSoporteModel> TICKETS_SOPORTE { get; set; }
        public DbSet<UltimoPedidoPorDniModel> V_UltimoPedidoPorDni { get; set; }
        public DbSet<AgTurnoEstudioModel> AG_TURNO_ESTUDIO { get; set; }
        // Sin tabla/vista propia: solo se consulta via FromSqlRaw (CompletarPracticaAsync en
        // ListadoTurnoCmd), contra el servidor enlazado SRV-DESA01.TWCC.dbo.V_MT_NOMENCLADOR.
        public DbSet<NomencladorPracticaModel> V_MT_NOMENCLADOR { get; set; }
        // Sin tabla/vista propia: solo se consulta via FromSqlRaw (CompletarRecomendadoAsync en
        // ListadoTurnoCmd), contra vistas del servidor enlazado SRV-DESA02 (MIND/ETL).
        public DbSet<RecomendacionTurnoModel> RecomendacionTurno { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelSolPractBhMetodoModel>().HasNoKey();
            modelBuilder.Entity<SolPractBhDto>().HasNoKey();
            modelBuilder.Entity<CacheSolPractBhMetodoModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractBhUnidadModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractBhServicioSolModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractBhOsModel>().HasNoKey();
            modelBuilder.Entity<BhEstudiosModel>().HasNoKey();
            modelBuilder.Entity<UltimoPedidoPorDniModel>().HasNoKey();
            modelBuilder.Entity<NomencladorPracticaModel>().HasNoKey();
            modelBuilder.Entity<RecomendacionTurnoModel>().HasNoKey();
            //modelBuilder.Entity<SolPractBhPedidoManualModel>().HasNoKey();
            modelBuilder.Entity<RelSolPractModel>().Property(e => e.IDESTUDIO_NUM).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<SolPractBhPedidoManualModel>().Property(e => e.IDESTUDIO_NUM).ValueGeneratedOnAddOrUpdate();
        }

    }

    public class DataBase2Context : DbContext
    {
        public DataBase2Context(DbContextOptions<DataBase2Context> options) : base(options)

        {


        }

        public DbSet<ListadoTurnosModel> vListadoTurnos { get; set; }
        public DbSet <AtencionesDiaModel> vMultiConsultaNatanet {  get; set; }
        // Sin tabla/vista propia: solo se consulta via FromSqlRaw (AtencionesPacienteCmd), contra
        // MOVENCA/MOVPRAC/Nomenclador/SERVICIOS (misma fuente que vMultiConsultaNatanet, pero
        // agregando el join a Nomenclador para resolver la practica que esa vista no expone).
        public DbSet<AtencionPacienteModel> AtencionesPaciente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListadoTurnosModel>()
                .HasNoKey();
            modelBuilder.Entity<AtencionesDiaModel>().HasNoKey();
            modelBuilder.Entity<AtencionPacienteModel>().HasNoKey();

        }
    }
    public class DataBase3Context : DbContext
    {
        public DataBase3Context(DbContextOptions<DataBase3Context> options) : base(options)
        {
        }
        public DbSet<InstructivoModel> V_BH_DESCRIPCIONES { get; set; }
        public DbSet<GrupoEstudiosModel> GRUPOESTUDIOS { get; set; }
        public DbSet<ObrasSocialesLaboModel> V_LABO_OS_MOSTRAR { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObrasSocialesLaboModel>()
                .HasNoKey();
            

        }
    }
}
