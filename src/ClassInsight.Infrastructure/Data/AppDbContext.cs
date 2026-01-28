using Microsoft.EntityFrameworkCore;
using ClassInsight.Domain.Entities;

namespace ClassInsight.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<RegistroAprendizagem> Registros { get; set; }
    public DbSet<TrilhaPedagogica> Trilhas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações adicionais de mapeamento (Ex: Value Objects como JSON)
        modelBuilder.Entity<RegistroAprendizagem>().OwnsOne(r => r.Analise);
        modelBuilder.Entity<RegistroAprendizagem>().OwnsOne(r => r.SugestaoPedagogica);
        
        modelBuilder.Entity<TrilhaPedagogica>().OwnsMany(t => t.Passos);
    }
}