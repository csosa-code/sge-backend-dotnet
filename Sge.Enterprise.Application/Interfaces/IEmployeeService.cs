using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Domain.Entities;

namespace Sge.Enterprise.Application.Interfaces;

public interface IEmployeeService
{
    Task<IReadOnlyList<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto> GetByIdAsync(int employeeId);
    Task<EmployeeDto> CreateAsync(EmployeeAddDto data);
    Task<EmployeeDto> UpdateAsync(int employeeId, EmployeeUpdateDto data);
    Task ActivateAsync(int employeeId);
    Task DeactivateAsync(int employeeId);
}