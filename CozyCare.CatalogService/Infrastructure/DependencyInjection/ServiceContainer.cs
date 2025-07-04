using CozyCare.CatalogService.Application.Interfaces;
using CozyCare.CatalogService.Application.Profiles;
using CozyCare.CatalogService.Application.Services;
using CozyCare.CatalogService.Infrastructure.DBContext;
using CozyCare.SharedKernel.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCare.CatalogService.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add db connectivity
            // Add authentication scheme
            SharedServiceContainer.AddSharedServices<CozyCareCatalogDbContext>(services, config, config["MySerilog:FineName"]!);

            // Create DI
            services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IServiceDetailService, ServiceDetailService>();
            services.AddScoped<IServiceService, ServiceService>();

            services.AddAutoMapper(typeof(CatalogMappingProfile).Assembly);

            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
