using Microsoft.EntityFrameworkCore;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Contexts;
using SalesSystem.Persistence.Interfaces;

namespace SalesSystem.Persistence.Repositories
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbVentaContext _context;
        public VentaRepository(DbVentaContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Venta> Register(Venta venta)
        {
            Venta ventaGenerada = new();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var detalleVenta in venta.DetalleVenta)
                {
                    var productoEncontrado = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == detalleVenta.IdProducto);
                    if (productoEncontrado is not null)
                    {
                        productoEncontrado.Stock -= detalleVenta.Cantidad;
                        _context.Productos.Update(productoEncontrado);
                    }
                }
                await _context.SaveChangesAsync();
                var correlativo = await _context.NumeroDocumentos.FirstAsync();
                correlativo.UltimoNumero++;
                correlativo.FechaRegistro = DateTime.Now;
                _context.NumeroDocumentos.Update(correlativo);
                await _context.SaveChangesAsync();
                int cantidadDigitos = 4;
                string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                string numeroventa = $"{ceros}{correlativo.UltimoNumero}";
                numeroventa = numeroventa.Substring(numeroventa.Length - cantidadDigitos, cantidadDigitos);
                venta.NumeroDocumento = numeroventa;
                await _context.Ventas.AddAsync(venta);
                await _context.SaveChangesAsync();
                ventaGenerada = venta;
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return ventaGenerada;
        }
    }
}