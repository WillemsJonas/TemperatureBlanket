using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemperatuurDeken;

namespace TemperatuurDekenLibrary.Repositories.Configurations;
public class DagConfiguration : IEntityTypeConfiguration<Dag>
{
    public void Configure(EntityTypeBuilder<Dag> builder)
    {
        builder.ToTable("Dagen");
        builder.HasIndex(b => b.Datum);

        builder.HasKey(b => b.DagId);
    }
}
