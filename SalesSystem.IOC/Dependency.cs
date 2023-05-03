using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.Persistence.Contexts;
using SalesSystem.Persistence.Interfaces;
using SalesSystem.Persistence.Repositories;
using SalesSystem.Utility;

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
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}