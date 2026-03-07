

using System.Security.Cryptography;
using System.Text;

namespace Sge.Enterprise.Application.Security;

public static class PasswordExtensions
{
    public static PasswordHashResult HashPassword(string password)
    {
        using var hmac = new HMACSHA512();

        return new PasswordHashResult
        {
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };
    }

    public static bool ValidatePassword(byte[] passwordHash, byte[] passwordSalt, string password)
    {
        using var hmac = new HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(
            Encoding.UTF8.GetBytes(password)
        );

        return computedHash.SequenceEqual(passwordHash);
    }
}