using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Infrastructure.Persistence;

namespace Sge.Enterprise.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SgeDbContext _context;

    public IEmployeeRepository Employees { get; }
    public IAreaRepository Areas { get; }

    public UnitOfWork(
        SgeDbContext context,
        IEmployeeRepository employeeRepository,
        IAreaRepository areaRepository
        )
    {
        _context = context;
        Employees = employeeRepository;
        Areas = areaRepository;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}