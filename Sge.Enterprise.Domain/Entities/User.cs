

using Sge.Enterprise.Domain.Common;

namespace Sge.Enterprise.Domain.Entities;

public class User : AuditableEntity
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public bool MustChangePassword { get; set; } = false;
}