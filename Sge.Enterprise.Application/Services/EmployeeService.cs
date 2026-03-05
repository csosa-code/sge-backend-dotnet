using Sge.Enterprise.Application.Interfaces;

using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using AutoMapper;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Settings;
using Microsoft.Extensions.Options;
using Sge.Enterprise.Application.Exceptions;
using Sge.Enterprise.Domain.Pagination;


namespace Sge.Enterprise.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly EmployeeSettings _employeeSettings;
    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<EmployeeSettings> employeeSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _employeeSettings = employeeSettings.Value;
    }

    public async Task<object> GetAllAsync(QueryParams? queryParams = null)

    {
        if (queryParams?.Page.HasValue == true && queryParams.PageSize.HasValue)
        {
            var pagination = await _unitOfWork.Employees.GetAllWithPaginationAsync(queryParams);

            return new PaginationResult<EmployeeDto>
            {
                Data = _mapper.Map<IEnumerable<EmployeeDto>>(pagination.Data),
                CurrentPage = pagination.CurrentPage,
                TotalItems = pagination.TotalItems,
                TotalPages = pagination.TotalPages
            };
        }

        var employees = await _unitOfWork.Employees.GetAllAsync();
        return _mapper.Map<IReadOnlyList<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> GetByIdAsync(int employeeId)
    {
        var employee = await GetEmployeeAsync(employeeId);
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateAsync(EmployeeAddDto data)
    {
        ValidateMinSalary(data.Salary);
        await ValidateArea(data.AreaId);
        await ValidateDocumentNumberAsync(data.DocumentNumber);

        var employee = _mapper.Map<Employee>(data);

        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> UpdateAsync(int employeeId, EmployeeUpdateDto data)
    {
        try
        {
            var curEmployee = await GetEmployeeAsync(employeeId);
            ValidateMinSalary(data.Salary);
            await ValidateArea(data.AreaId);
            await ValidateDocumentNumberAsync(data.DocumentNumber, employeeId);

            _mapper.Map(data, curEmployee);

            await _unitOfWork.Employees.UpdateAsync(curEmployee);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<EmployeeDto>(curEmployee);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task ActivateAsync(int employeeId)
    {
        var employee = await GetEmployeeAsync(employeeId);

        if (employee.StatusId == 1)
            throw new BadRequestException("El empleado ya está activo");

        employee.StatusId = 1;
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeactivateAsync(int employeeId)
    {
        var employee = await GetEmployeeAsync(employeeId);

        if (employee.StatusId == 0)
            throw new BadRequestException("El empleado ya está inactivo");

        employee.StatusId = 0;
        await _unitOfWork.CompleteAsync();
    }

    private void ValidateMinSalary(decimal salary)
    {
        if (salary < _employeeSettings.MinSalary)
            throw new BadRequestException(
                $"El salario no puede ser menor a {_employeeSettings.MinSalary}.");
    }

    private async Task<Area> ValidateArea(int areaId)
    {
        return await _unitOfWork.Areas.GetByIdAsync(areaId) ?? throw new NotFoundException("Área no encontrada");
    }

    private async Task<Employee> GetEmployeeAsync(int employeeId)
    {
        return await _unitOfWork.Employees.GetByIdAsync(employeeId) ?? throw new NotFoundException("Empleado no encontrado");
    }

    private async Task ValidateDocumentNumberAsync(string documentNumber, int? employeeId = null)
    {
        var exists = await _unitOfWork.Employees
            .ExistsByDocumentNumberAsync(documentNumber, employeeId);

        if (exists)
            throw new BadRequestException("El número de documento ya está en uso.");
    }
}