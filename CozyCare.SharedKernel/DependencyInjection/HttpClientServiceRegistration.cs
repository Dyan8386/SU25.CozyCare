using CozyCare.SharedKernel.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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

            // Common DelegatingHandler, SocketsHttpHandler, Polly policies…
            services.AddTransient<LoggingHandler>();

            foreach (var svc in serviceRoutes)
            {
                var name = svc.Key;                // e.g. "CatalogService"
                var prefix = (svc.Value ?? throw new InvalidOperationException($"Missing route for service: {svc.Key}"))
                .TrimEnd('/');

                services
                    .AddHttpClient(name, client =>
                    {
                        client.BaseAddress = new Uri($"{gatewayBase}{prefix}");
                        client.Timeout = TimeSpan.FromSeconds(30);
                        client.DefaultRequestHeaders.Accept
                              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    })
                    .ConfigurePrimaryHttpMessageHandler(_ => CreateSocketsHandler())
                    .AddHttpMessageHandler<LoggingHandler>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(10));
            }

            return services;
        }

        private static SocketsHttpHandler CreateSocketsHandler()
        {
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),    // tái resolve DNS sau 5 phút
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2), // đóng kết nối lúc idle
                ConnectTimeout = TimeSpan.FromSeconds(10),             // timeout kết nối TCP
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }
    }

}
