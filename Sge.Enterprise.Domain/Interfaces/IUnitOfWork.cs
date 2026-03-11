namespace Sge.Enterprise.Domain.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IEmployeeRepository Employees { get; }
    IAreaRepository Areas { get; }
    IUserRepository Users { get; }

    Task<int> CompleteAsync();
}