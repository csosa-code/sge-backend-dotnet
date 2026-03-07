using Microsoft.EntityFrameworkCore;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Infrastructure.Persistence;

namespace Sge.Enterprise.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SgeDbContext _context;

    public UserRepository(SgeDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

    public async Task<IReadOnlyList<User>> GetAllAsync()
        => await _context.Users
            .AsNoTracking()
            .ToListAsync();

    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public Task UpdateAsync(User user)
    {
        return Task.CompletedTask;
    }

    public async Task<User?> GetByUsernameAsync(string username)
        => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
}

