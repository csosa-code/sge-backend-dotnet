namespace Sge.Enterprise.Application.Services;
using Sge.Enterprise.Application.Interfaces;
public class ServiceManager : IServiceManager
{
    public IEmployeeService EmployeeService { get; }
    public IAreaService AreaService { get; }

    public ServiceManager(IEmployeeService employeeService, IAreaService areaService)
    {
        EmployeeService = employeeService;
        AreaService = areaService;
    }
}