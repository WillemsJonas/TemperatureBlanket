using Microsoft.EntityFrameworkCore;
using TemperatuurDeken;
using TemperatuurDekenLibrary.Repositories.Configurations;
using TemperatuurDekenLibrary.Repositories.Seedings;

namespace TemperatuurDekenLibrary;

public class TemperatuurDekenContext : DbContext
{
    public TemperatuurDekenContext(DbContextOptions<TemperatuurDekenContext> options) : base(options) { }
    public DbSet<Dag> Dagen => Set<Dag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DagConfiguration());
        modelBuilder.ApplyConfiguration(new DagSeeding());

        base.OnModelCreating(modelBuilder);
    }
}
