using CozyCare.BookingService.Application.Externals;
using CozyCare.BookingService.Applications.Externals;
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


            //Đăng ký Profile 
            services.AddAutoMapper(typeof(BookingProfile).Assembly, 
                typeof(BookingDetailProfile).Assembly, 
                typeof(BookingStatusProfile).Assembly);

			services.AddHttpClient<ICatalogApiClient, CatalogApiClient>("CatalogService")
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					AllowAutoRedirect = false
				});

            services.AddHttpClient<IIdentityApiClient, IdentityApiClient>("IdentityService")
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					AllowAutoRedirect = false
				});
			services.AddHttpClient<IPaymentApiClient, PaymentApiClient>("PaymentService")
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					AllowAutoRedirect = false
				});
			services.AddHttpClient<IJobApiClient, JobApiClient>("JobService")
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					AllowAutoRedirect = false
				});

			return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
