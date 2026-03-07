namespace Sge.Enterprise.Domain.Interfaces;

using Sge.Enterprise.Domain.Entities;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<IReadOnlyList<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}