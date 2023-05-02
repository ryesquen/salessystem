using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.Persistence.Contexts;

namespace SalesSystem.IOC
{
    public static class Dependency
    {
        public static void InjectionDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbVentaContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("cn"));
                });
        }
    }
}