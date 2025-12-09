using Sge.Enterprise.Domain.Common;

namespace Sge.Enterprise.Domain.Entities;

public class Employee : AuditableEntity
{
    public int EmployeeId { get; set; }
    public int AreaId { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DocumentNumber { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }

    public Area? Area { get; set; }
}