using Microsoft.AspNetCore.Mvc;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Interfaces;

namespace Sge.Enterprise.Api.Controllers;

public class DashboardController : BaseApiController
{
    private readonly IServiceManager _serviceManager;

    public DashboardController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var data = await _serviceManager.DashboardService.GetSummaryAsync();

        return Ok(data);
    }

    [HttpGet("payroll")]
    public async Task<IActionResult> GetPayroll()
    {
        var data = await _serviceManager.EmployeeService.GetAllAsync();

        return Ok(data);
    }
}

