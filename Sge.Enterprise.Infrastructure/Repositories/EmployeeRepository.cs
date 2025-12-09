using Microsoft.EntityFrameworkCore;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Infrastructure.Persistence;

namespace Sge.Enterprise.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly SgeDbContext _context;

    public EmployeeRepository(SgeDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(int id)
        => await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);

    public async Task<IReadOnlyList<Employee>> GetAllAsync()
        => await _context.Employees
            .AsNoTracking()
            .Include(e => e.Area)
            .ToListAsync();

    public async Task AddAsync(Employee employee)
        => await _context.Employees.AddAsync(employee);

    public Task UpdateAsync(Employee employee)
    {
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByDocumentNumberAsync(string documentNumber, int? excludeEmployeeId = null)
    {
        var query = _context.Employees
            .AsNoTracking()
            .Where(e => e.DocumentNumber == documentNumber);

        if (excludeEmployeeId.HasValue)
        {
            query = query.Where(e => e.EmployeeId != excludeEmployeeId.Value);
        }

        return await query.AnyAsync();
    }
}