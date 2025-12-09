

using Sge.Enterprise.Domain.Common;

namespace Sge.Enterprise.Domain.Entities;

public class Area : AuditableEntity
{
    public int AreaId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}