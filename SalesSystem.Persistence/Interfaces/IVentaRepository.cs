using SalesSystem.Domain.Entities;

namespace SalesSystem.Persistence.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Register(Venta venta);
    }
}
