using CozyCare.SharedKernel.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CozyCare.SharedKernel.DependencyInjection
{
    public static class JWTAuthenticationScheme
    {
        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
        {
            // 1. Bind JwtSettings trực tiếp từ config
            var jwt = new JwtSettings();
            config.GetSection("Authentication").Bind(jwt);


            // 2. Tạo key bytes
            var keyBytes = Encoding.UTF8.GetBytes(jwt.Key);

            // 3. Đăng ký JWT Bearer
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
