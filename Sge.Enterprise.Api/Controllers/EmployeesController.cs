using Microsoft.AspNetCore.Mvc;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Interfaces;
using Sge.Enterprise.Domain.Pagination;

namespace Sge.Enterprise.Api.Controllers;

public class EmployeesController : BaseApiController
{
    private readonly IServiceManager _serviceManager;

    public EmployeesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryParams queryParams)
    {
        Console.WriteLine($"Page: {queryParams.Page}");
Console.WriteLine($"PageSize: {queryParams.PageSize}");
Console.WriteLine($"Sort: {queryParams.SortActive}");
        var employees = await _serviceManager.EmployeeService.GetAllAsync(queryParams);
        return Ok(employees); 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _serviceManager.EmployeeService.GetByIdAsync(id);
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] EmployeeAddDto data)
    {
        var employee = await _serviceManager.EmployeeService.CreateAsync(data);
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmployeeUpdateDto data)
    {
        var employee = await _serviceManager.EmployeeService.UpdateAsync(id,data);
        return Ok(employee);
    }

    [HttpPut("{id}/activate")]
    public async Task<IActionResult> Activate(int id)
    {
        await _serviceManager.EmployeeService.ActivateAsync(id);
        return Ok();
    }

    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _serviceManager.EmployeeService.DeactivateAsync(id);
        return Ok();
    }
}