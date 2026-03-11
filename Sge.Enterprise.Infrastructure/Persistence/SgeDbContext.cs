using Microsoft.EntityFrameworkCore;
using Sge.Enterprise.Domain.Common;
using Sge.Enterprise.Domain.Entities;

namespace Sge.Enterprise.Infrastructure.Persistence;

public class SgeDbContext : DbContext
{
    public SgeDbContext(DbContextOptions<SgeDbContext> options) : base(options) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddAuditInfo()
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = "system";
                entry.Entity.StatusId = 1;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = "system";
            }
        }
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgeDbContext).Assembly);
        base.OnModelCreating(modelBuilder);  
    }


}