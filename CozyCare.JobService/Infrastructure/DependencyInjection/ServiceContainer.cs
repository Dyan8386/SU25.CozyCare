
using CozyCare.JobService.Application.Externals;
using CozyCare.JobService.Application.Interfaces;
using CozyCare.JobService.Application.Profiles;
using CozyCare.JobService.Application.Services;
using CozyCare.JobService.Infrastructure.DBContext;
using CozyCare.SharedKernel.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCare.JobService.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add db connectivity
            // Add authentication scheme
            SharedServiceContainer.AddSharedServices<CozyCareJobDbContext>(services, config, config["MySerilog:FineName"]!);

            // Create DI
            services.AddScoped<IJobUnitOfWork,JobUnitOfWork>();
            //services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ITaskClaimService, TaskClaimService>();
            services.AddScoped<ITaskClaimStatusService, TaskClaimStatusService>();

            //services.AddAutoMapper(typeof(CatalogMappingProfile).Assembly);
            //services.AddHttpClient<IIdentityApiClient, IdentityApiClient>("IdentityService");
            services.AddAutoMapper(typeof(JobMappingProfile).Assembly);
            services.AddHttpClient<IJobApiClient, JobApiClient>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
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
