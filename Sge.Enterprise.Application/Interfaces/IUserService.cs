using Sge.Enterprise.Application.Dtos;

namespace Sge.Enterprise.Application.Interfaces;

public interface IUserService
{
    Task<AuthResponseDto> LoginAsync(LoginDto data);
    Task<RegisterDto> RegisterAsync(RegisterDto data);
}

