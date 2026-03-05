using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Domain.Pagination;

namespace Sge.Enterprise.Application.Interfaces;

public interface IEmployeeService
{
    Task<object> GetAllAsync(QueryParams? queryParams = null);
    Task<EmployeeDto> GetByIdAsync(int employeeId);
    Task<EmployeeDto> CreateAsync(EmployeeAddDto data);
    Task<EmployeeDto> UpdateAsync(int employeeId, EmployeeUpdateDto data);
    Task ActivateAsync(int employeeId);
    Task DeactivateAsync(int employeeId);
}