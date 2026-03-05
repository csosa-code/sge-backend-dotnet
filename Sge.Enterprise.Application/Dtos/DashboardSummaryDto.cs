namespace Sge.Enterprise.Application.Dtos;

public class DashboardSummaryDto
{
    public int TotalEmployees { get; set; }

    public int TotalDepartments { get; set; }

    public decimal MonthlyPayroll { get; set; }

    public int ActiveEmployees { get; set; }

    public int InactiveEmployees { get; set; }

    public int ActivePercentage { get; set; }

    public int InactivePercentage { get; set; }
}

