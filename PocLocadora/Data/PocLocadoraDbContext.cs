using Microsoft.EntityFrameworkCore;
using PocLocadora.Models;

namespace PocLocadora.Data
{
    public class PocLocadoraDbContext : DbContext
    {
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Genero> Genero { get; set; }

        public PocLocadoraDbContext(DbContextOptions<PocLocadoraDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Filme>().HasOne(x => x.Genero).WithMany().HasForeignKey(x => x.GeneroId);
        }
    }
}
