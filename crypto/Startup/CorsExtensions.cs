using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace crypto.Startup
{
    public static class CorsExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy.WithOrigins("http://localhost:5173")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            return services;
        }
    }
}