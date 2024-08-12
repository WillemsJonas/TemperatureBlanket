using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperatuurDeken;

namespace TemperatuurDekenLibrary.Repositories.Configurations;
public class DayConfiguration : IEntityTypeConfiguration<Day>
{
    public void Configure(EntityTypeBuilder<Day> builder)
    {
        builder.ToTable("Dagen");
        builder.HasIndex(b => b.Date);

        builder.HasKey(b => b.DayId);
    }
}
