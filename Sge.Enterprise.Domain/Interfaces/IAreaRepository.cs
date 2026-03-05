namespace Sge.Enterprise.Domain.Interfaces;

using Sge.Enterprise.Domain.Entities;

public interface IAreaRepository
{
    Task<Area?> GetByIdAsync(int id);
    Task<IReadOnlyList<Area>> GetAllAsync();
    Task AddAsync(Area area);
    Task UpdateAsync(Area area);
    Task<bool> ExistsByNameAsync(string name);
    Task<int> CountAsync();
}