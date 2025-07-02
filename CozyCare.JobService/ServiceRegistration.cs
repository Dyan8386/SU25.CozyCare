
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CozyCare.JobService.Infrastructure.DBContext;
using CozyCare.JobService.Infrastructure;

namespace CozyCare.CatalogService;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Đăng ký DbContext với chuỗi kết nối từ appsettings.json
        services.AddDbContext<CozyCareJobDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Đăng ký UnitOfWork
        services.AddScoped<IJobUnitOfWork, JobUnitOfWork>();

        return services;
    }
}
