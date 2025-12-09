using Microsoft.AspNetCore.Mvc;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Interfaces;

namespace Sge.Enterprise.Api.Controllers;

public class AreasController : BaseApiController
{
    private readonly IServiceManager _serviceManager;

    public AreasController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var areas = await _serviceManager.AreaService.GetAllAsync();
        return Ok(areas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var area = await _serviceManager.AreaService.GetByIdAsync(id);
        return Ok(area);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AreaAddDto data)
    {
        var area = await _serviceManager.AreaService.CreateAsync(data);
        return Ok(area);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AreaUpdateDto data)
    {
        var area = await _serviceManager.AreaService.UpdateAsync(id, data);
        return Ok(area);
    }
}

