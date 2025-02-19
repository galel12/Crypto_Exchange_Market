using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using crypto.Services;
using crypto.Repositories;
using crypto.Data;


namespace crypto.Startup
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, string postgresConnection)
        {
            // enable async suffix in action names for controllers 
            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(postgresConnection)
                       .LogTo(Console.WriteLine, LogLevel.Information));

            // Register Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}