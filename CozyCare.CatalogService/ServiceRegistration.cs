using CozyCare.CatalogService.Infrastructure.DBContext;
using CozyCare.CatalogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.CatalogService.Application.Services;

namespace CozyCare.CatalogService;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Đăng ký DbContext với chuỗi kết nối từ appsettings.json
        services.AddDbContext<CozyCareCatalogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Đăng ký UnitOfWork
        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IServiceDetailService, ServiceDetailService>();
        services.AddScoped<IServiceService, ServiceService>();

        // Đăng ký AutoMapper với tất cả Profile trong assembly hiện tại
        services.AddAutoMapper(typeof(ServiceRegistration).Assembly);
        return services;
    }
}
