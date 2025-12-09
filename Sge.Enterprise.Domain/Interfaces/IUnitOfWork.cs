namespace Sge.Enterprise.Domain.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IEmployeeRepository Employees { get; }
    IAreaRepository Areas { get; }

    Task<int> CompleteAsync();
}