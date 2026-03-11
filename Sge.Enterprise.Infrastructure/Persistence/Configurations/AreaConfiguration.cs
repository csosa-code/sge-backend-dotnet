using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sge.Enterprise.Domain.Entities;

namespace Sge.Enterprise.Infrastructure.Persistence.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Areas");

        builder.HasKey(a => a.AreaId);

        builder.HasIndex(a => a.Name)
            .IsUnique();
    }
}