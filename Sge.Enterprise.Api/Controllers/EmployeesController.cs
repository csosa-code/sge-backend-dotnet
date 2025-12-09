using Microsoft.AspNetCore.Mvc;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Interfaces;

namespace Sge.Enterprise.Api.Controllers;

public class EmployeesController : BaseApiController
{
    private readonly IServiceManager _serviceManager;

    public EmployeesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employees = await _serviceManager.EmployeeService.GetAllAsync();
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
}