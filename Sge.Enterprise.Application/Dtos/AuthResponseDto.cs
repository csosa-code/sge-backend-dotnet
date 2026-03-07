namespace Sge.Enterprise.Application.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}

