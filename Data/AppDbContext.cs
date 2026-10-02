using Microsoft.EntityFrameworkCore;
using MonitoramentoEnergeticoAPI.Models;

namespace MonitoramentoEnergeticoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<ConsumoEnergia> ConsumosEnergia { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
        public DbSet<AlertaEnergia> AlertasEnergia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Setor>()
                .Property(x => x.LimiteConsumo)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ConsumoEnergia>()
                .Property(x => x.ConsumoKwh)
                .HasPrecision(18, 2);

            
            modelBuilder.Entity<AlertaEnergia>()
                .Property(x => x.Mensagem)
                .HasMaxLength(300);
        }
    }
}
