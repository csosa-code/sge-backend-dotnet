namespace Sge.Enterprise.Application.Dtos;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = null!;
    public string DocumentNumber { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }

    public int AreaId { get; set; }
    public string? AreaName { get; set; }
}