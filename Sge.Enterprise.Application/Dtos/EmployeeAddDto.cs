namespace Sge.Enterprise.Application.Dtos;

public class EmployeeAddDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DocumentNumber { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }
    public int AreaId { get; set; }
}