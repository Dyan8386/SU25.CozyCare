using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.IdentityService.Application.Profiles;
using CozyCare.IdentityService.Application.Services;
using CozyCare.IdentityService.Infrastructure.DBContext;
using CozyCare.SharedKernel.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCare.IdentityService.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add db connectivity
            // Add authentication scheme
            SharedServiceContainer.AddSharedServices<CozyCareIdentityDbContext>(services, config, config["MySerilog:FineName"]!);

            // Create DI
            services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //services.AddScoped<IServiceService, ServiceService>();

            services.AddAutoMapper(typeof(IdentityMappingProfile).Assembly);

            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
