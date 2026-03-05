namespace Sge.Enterprise.Domain.Interfaces;

using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Pagination;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id);
    Task<IReadOnlyList<Employee>> GetAllAsync();
    Task<PaginationResult<Employee>> GetAllWithPaginationAsync(QueryParams queryParams);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
     Task<bool> ExistsByDocumentNumberAsync(string documentNumber, int? excludeEmployeeId = null);
     Task<int> CountAsync();
     Task<int> CountByStatusAsync(int statusId);
     Task<decimal> SumSalaryAsync();
}