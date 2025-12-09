namespace Sge.Enterprise.Application.Interfaces;

public interface IServiceManager
{
    IEmployeeService EmployeeService { get; }
    IAreaService AreaService { get; }
}