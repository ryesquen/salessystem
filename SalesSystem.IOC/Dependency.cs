using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.Application.Interfaces;
using SalesSystem.Application.Services;
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

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
        }
    }
}