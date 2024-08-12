using Microsoft.EntityFrameworkCore;
using TemperatuurDeken;
using TemperatuurDekenLibrary.Repositories.Configurations;
using TemperatuurDekenLibrary.Repositories.Seedings;

namespace TemperatuurDekenLibrary;

public class TemperatureBlanketContext : DbContext
{
    public TemperatureBlanketContext(DbContextOptions<TemperatureBlanketContext> options) : base(options) { }
    public DbSet<Day> Days => Set<Day>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DayConfiguration());
        modelBuilder.ApplyConfiguration(new DaySeeding());

        base.OnModelCreating(modelBuilder);
    }
}
