using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.SharedKernel.DependencyInjection
{
    public static class HttpClientServiceRegistration
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            var gatewayBase = config["ApiGateway:BaseUrl"]!;
            var serviceRoutes = config.GetSection("Services").GetChildren();

            foreach (var service in serviceRoutes)
            {
                var serviceName = service.Key;
                var routePrefix = service.Value?.TrimEnd('/');

                if (!string.IsNullOrWhiteSpace(routePrefix))
                {
                    services.AddHttpClient(serviceName, client =>
                    {
                        client.BaseAddress = new Uri($"{gatewayBase}{routePrefix}");
                    });
                }
            }

            return services;
        }
    }

}
