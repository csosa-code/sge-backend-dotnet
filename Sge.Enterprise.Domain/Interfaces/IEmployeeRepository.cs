namespace Sge.Enterprise.Domain.Interfaces;

using Sge.Enterprise.Domain.Entities;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id);
    Task<IReadOnlyList<Employee>> GetAllAsync();
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
     Task<bool> ExistsByDocumentNumberAsync(string documentNumber, int? excludeEmployeeId = null);
}