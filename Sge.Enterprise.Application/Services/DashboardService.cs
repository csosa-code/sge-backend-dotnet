using Sge.Enterprise.Application.Interfaces;
using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Application.Dtos;

namespace Sge.Enterprise.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var totalEmployees = await _unitOfWork.Employees.CountAsync();
        var totalDepartments = await _unitOfWork.Areas.CountAsync();

        var activeEmployees = await _unitOfWork.Employees.CountByStatusAsync(1);
        var inactiveEmployees = await _unitOfWork.Employees.CountByStatusAsync(0);

        var monthlyPayroll = await _unitOfWork.Employees.SumSalaryAsync();

        var total = activeEmployees + inactiveEmployees;

        return new DashboardSummaryDto
        {
            TotalEmployees = totalEmployees,
            TotalDepartments = totalDepartments,
            MonthlyPayroll = monthlyPayroll,
            ActiveEmployees = activeEmployees,
            InactiveEmployees = inactiveEmployees,
            ActivePercentage = total == 0 ? 0 : (activeEmployees * 100) / total,
            InactivePercentage = total == 0 ? 0 : (inactiveEmployees * 100) / total
        };
    }
}

