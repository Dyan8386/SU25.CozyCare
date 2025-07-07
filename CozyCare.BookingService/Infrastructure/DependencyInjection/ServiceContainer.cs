using CozyCare.BookingService.Applications.External;
using CozyCare.BookingService.Applications.Interfaces;
using CozyCare.BookingService.Applications.Profiles;
using CozyCare.BookingService.Applications.Services;
using CozyCare.BookingService.Infrastructure;
using CozyCare.BookingService.Infrastructure.DBContext;
using CozyCare.SharedKernel.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCare.BookingService.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add db connectivity
            // Add authentication scheme
            SharedServiceContainer.AddSharedServices<CozyCareBookingDbContext>(services, config, config["MySerilog:FineName"]!);

			// Create DI
			// Đăng ký UnitOfWork
			services.AddScoped<IBookingUnitOfWork, BookingUnitOfWork>();

			// Đăng ký các dịch vụ liên quan đến Booking
			services.AddScoped<IBookingService, Applications.Services.BookingService>();
			services.AddScoped<IBookingDetailService, BookingDetailService>();
			services.AddScoped<IBookingStatusService, BookingStatusService>();
            services.AddScoped<ICatalogApiClient, CatalogApiClient>();


            //Đăng ký Profile 
            services.AddAutoMapper(typeof(BookingProfile).Assembly, 
                typeof(BookingDetailProfile).Assembly, 
                typeof(BookingStatusProfile).Assembly);

			return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
