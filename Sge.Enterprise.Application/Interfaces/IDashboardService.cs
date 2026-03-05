using Sge.Enterprise.Application.Dtos;

namespace Sge.Enterprise.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}

