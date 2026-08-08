using Microsoft.EntityFrameworkCore;
using ValorModels.Models.PchimModels;

namespace ValoresData.Context
{
    public class PchimContext : DbContext
    {
        public PchimContext(DbContextOptions<PchimContext> options) : base(options) { }

        public DbSet<Estudios> Estudios { get; set; }
        public DbSet<VEstudios> VEstudios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VEstudios>()
                .HasNoKey()
                .ToView("V_ESTUDIOS_MASTER", "dbo");
        }
    }
}
