using CozyCare.BookingService.Infrastructure.DBContext;
using CozyCare.BookingService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCare.BookingService;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Đăng ký DbContext với chuỗi kết nối từ appsettings.json
        services.AddDbContext<CozyCareBookingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Đăng ký UnitOfWork
        services.AddScoped<IBookingUnitOfWork, BookingUnitOfWork>();

        return services;
    }
}
