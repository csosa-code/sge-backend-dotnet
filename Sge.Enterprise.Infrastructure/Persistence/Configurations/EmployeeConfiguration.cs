using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sge.Enterprise.Domain.Entities;

namespace Sge.Enterprise.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.EmployeeId);

        builder.HasIndex(e => e.DocumentNumber)
            .IsUnique();

        builder.Property(e => e.Salary)
            .HasPrecision(18, 2);
    }
}