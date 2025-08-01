﻿using CozyCare.PaymentService.Application.Externals;
using CozyCare.PaymentService.Application.Interfaces;
using CozyCare.PaymentService.Application.Profiles;
using CozyCare.PaymentService.Application.Services;
using CozyCare.PaymentService.Infrastructure.DBContext;
using CozyCare.SharedKernel.DependencyInjection;
using CozyCare.ViewModels.Momo;
using Microsoft.Extensions.DependencyInjection;

namespace CozyCare.PaymentService.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add db connectivity
            // Add authentication scheme
            SharedServiceContainer.AddSharedServices<CozyCarePaymentDbContext>(services, config, config["MySerilog:FineName"]!);

            // Create DI
            services.AddScoped<IPaymentUnitOfWork, PaymentUnitOfWork>();
            //services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IBookingApiClient, BookingApiClient>();
            services.AddScoped<IMomoService, MomoService>();
            services.AddHttpClient<IBookingApiClient, BookingApiClient>("BookingService").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AllowAutoRedirect = false
            }); ;
            services.AddHttpClient<IMomoService, MomoService>();
            services.AddAutoMapper(typeof(PaymentMappingProfile).Assembly); 
            services.Configure<MomoOptionModel>(config.GetSection("MomoApi"));

            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
