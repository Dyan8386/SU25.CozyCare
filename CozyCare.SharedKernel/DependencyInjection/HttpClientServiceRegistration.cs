using CozyCare.SharedKernel.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace CozyCare.SharedKernel.DependencyInjection
{
    public static class HttpClientServiceRegistration
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            // Lấy base URL và đảm bảo không có slash ở cuối
            var gatewayBase = config["ApiGateway:BaseUrl"]?.TrimEnd('/')
                              ?? throw new InvalidOperationException("Missing ApiGateway:BaseUrl configuration");

            // Lấy các định nghĩa service từ cấu hình
            var serviceRoutes = config.GetSection("Services").GetChildren();

            // Đăng ký DelegatingHandler chung (ví dụ: logging, retry, circuit-breaker…)
            services.AddTransient<LoggingHandler>();

            foreach (var svc in serviceRoutes)
            {
                var name = svc.Key; // Ví dụ: "CatalogService"
                var rawPrefix = svc.Value
                                ?? throw new InvalidOperationException($"Missing route prefix for service '{name}'");

                // Chuẩn hóa prefix: loại bỏ slash thừa và thêm slash ở cuối
                var prefix = rawPrefix.Trim('/').ToLowerInvariant() + "/";

                services
                    .AddHttpClient(name, client =>
                    {
                        // Kết quả: https://localhost:5158/{prefix}/
                        client.BaseAddress = new Uri($"{gatewayBase}/{prefix}");
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
                // Tái resolve DNS sau 5 phút
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                // Đóng kết nối khi idle 2 phút
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
                // Timeout kết nối TCP
                ConnectTimeout = TimeSpan.FromSeconds(10),
                // Hỗ trợ giải nén GZip/Deflate
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }
    }
}
