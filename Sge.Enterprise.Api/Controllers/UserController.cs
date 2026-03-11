using Microsoft.AspNetCore.Mvc;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Sge.Enterprise.Api.Controllers;
[Authorize]
public class UserController : BaseApiController
{
    private readonly IServiceManager _serviceManager;

    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto data)
    {
        var authResponse = await _serviceManager.UserService.LoginAsync(data);
        return Ok(authResponse);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto data)
    {
        var user = await _serviceManager.UserService.RegisterAsync(data);
        return Ok(user);
    }
}

