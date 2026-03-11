namespace Sge.Enterprise.Application.Services;
using Sge.Enterprise.Application.Interfaces;
public class ServiceManager : IServiceManager
{
    public IEmployeeService EmployeeService { get; }
    public IAreaService AreaService { get; }
    public IDashboardService DashboardService { get; }
    public IUserService UserService { get; }
    public ServiceManager(IEmployeeService employeeService, IAreaService areaService, IDashboardService dashboardService, IUserService userService)
    {
        EmployeeService = employeeService;
        AreaService = areaService;
        DashboardService = dashboardService;
        UserService = userService;
    }
}