using Microsoft.EntityFrameworkCore;
using Sge.Enterprise.Domain.Pagination;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Infrastructure.Extensions;
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

    public async Task<PaginationResult<Employee>> GetAllWithPaginationAsync(QueryParams queryParams)
    {
        var query = _context.Employees
            .Include(e => e.Area)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Filter))
        {
            var filter = queryParams.Filter!.SanitizationFilter();

            var searchTerms = filter.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            query = query.Where(x =>
                searchTerms.All(term =>
                    EF.Functions.Collate(x.FirstName, "Latin1_general_CI_AI").Contains(term)
                    || EF.Functions.Collate(x.LastName, "Latin1_general_CI_AI").Contains(term)
                    || EF.Functions.Collate(x.DocumentNumber, "Latin1_general_CI_AI").Contains(term)
                )
            );
        }

        query = query.ApplySort(queryParams.SortActive, queryParams.SortDirection);

        var totalItems = await query.CountAsync();

        var totalPages = (int)Math.Ceiling((double)totalItems / queryParams.PageSize!.Value);

        var data = await query
            .ApplyPagination(queryParams.Page!.Value, queryParams.PageSize.Value)
            .ToListAsync();

        return new PaginationResult<Employee>
        {
            Data = data,
            CurrentPage = queryParams.Page.Value,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

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

    public async Task<int> CountAsync()
    {
        return await _context.Employees.CountAsync();
    }

    public async Task<int> CountByStatusAsync(int statusId)
    {
        return await _context.Employees
            .Where(e => e.StatusId == statusId)
            .CountAsync();
    }

    public async Task<decimal> SumSalaryAsync()
    {
        return await _context.Employees
            .Where(e => e.StatusId == 1)
            .SumAsync(e => e.Salary);
    }
}