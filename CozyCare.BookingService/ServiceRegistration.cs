using CozyCare.BookingService.Infrastructure.DBContext;
using CozyCare.BookingService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.Applications.Services;


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

		// Đăng ký các dịch vụ liên quan đến Booking
		services.AddScoped<IBookingService, Applications.Services.BookingService>();
		services.AddScoped<IBookingDetailService, BookingDetailService>();
		services.AddScoped<IBookingStatusService, BookingStatusService>();

		//Đăng ký Profile 
		services.AddAutoMapper(typeof(ServiceRegistration).Assembly);
        return services;
    }
}
