using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repo
{
    public class HeroiContext : DbContext
    {
        public DbSet<Heroi> Heroi { get; set; }
        public DbSet<Arma> Arma { get; set; }
        public DbSet<Batalha> Batalha { get; set; }
        public DbSet<HeroiBatalha> HeroiBatalhas { get; set; }
        public DbSet<IdentidadeSecreta> IdentidadeSecreta { get; set; }

        public HeroiContext(DbContextOptions<HeroiContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity =>
            {
                entity.HasKey(e => new {e.BatalhaId,e.HeroiId });
            });
        }

    }
}
