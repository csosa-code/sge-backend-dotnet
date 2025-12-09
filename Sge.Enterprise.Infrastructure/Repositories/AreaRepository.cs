using Microsoft.EntityFrameworkCore;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Infrastructure.Persistence;

namespace Sge.Enterprise.Infrastructure.Repositories;

public class AreaRepository : IAreaRepository
{
    private readonly SgeDbContext _context;

    public AreaRepository(SgeDbContext context)
    {
        _context = context;
    }

    public async Task<Area?> GetByIdAsync(int id)
        => await _context.Areas.FirstOrDefaultAsync(a => a.AreaId == id);

    public async Task<IReadOnlyList<Area>> GetAllAsync()
        => await _context.Areas
            .AsNoTracking()
            .ToListAsync();

    public async Task AddAsync(Area area)
        => await _context.Areas.AddAsync(area);

    public Task UpdateAsync(Area area)
    {
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Areas
            .AnyAsync(a => a.Name.ToLower() == name.ToLower());
    }

    


}

