namespace Sge.Enterprise.Application.Security;

public class PasswordHashResult
{
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
}