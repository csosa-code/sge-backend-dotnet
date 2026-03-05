using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sge.Enterprise.Application.Interfaces;
using Sge.Enterprise.Application.Services;
using Sge.Enterprise.Domain.Interfaces;
using Sge.Enterprise.Infrastructure.Persistence;
using Sge.Enterprise.Infrastructure.Repositories;

namespace Sge.Enterprise.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SgeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        AddRepositories(services);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        AddServices(services);
        services.AddScoped<IServiceManager, ServiceManager>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IAreaRepository, AreaRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IDashboardService, DashboardService>();
        return services;
    }
}