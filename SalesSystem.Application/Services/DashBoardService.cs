using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;
using System.Globalization;

namespace SalesSystem.Application.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;

        public DashBoardService(IVentaRepository ventaRepository, IGenericRepository<Producto> productoRepository)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
        }

        public DashBoardDto Resume()
        {
            var dashboard = new DashBoardDto();
            try
            {
                dashboard.TotalVentas = TotalVentasUltimaSemana();
                dashboard.TotalIngresos = TotalIngresosUltimaSemana();
                dashboard.TotalProductos = TotalProductos();
                var listaVentasSemana = new List<VentaSemanaDto>();
                foreach (var item in VentasUltimaSemana())
                {
                    listaVentasSemana.Add(new VentaSemanaDto
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }
                dashboard.VentasUltimaSemana = listaVentasSemana;
                return dashboard;
            }
            catch { throw; }
        }
        private static IQueryable<Venta> ReturnSales(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
            ultimaFecha = ultimaFecha!.Value.AddDays(restarCantidadDias);
            return tablaVenta.Where(v => v.FechaRegistro!.Value.Date >= ultimaFecha.Value.Date);
        }
        private int TotalVentasUltimaSemana()
        {
            int total = 0;
            var _ventaQuery = _ventaRepository.Query();
            if (_ventaQuery.Any())
            {
                var tablaVenta = ReturnSales(_ventaQuery, -7);
                total = tablaVenta.Count();
            }
            return total;
        }
        private string TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            var _ventaQuery = _ventaRepository.Query();
            if (_ventaQuery.Any())
            {
                var tablaVenta = ReturnSales(_ventaQuery, -7);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v!.Value);
            }
            return Convert.ToString(resultado, new CultureInfo("es-PE"));
        }
        private int TotalProductos()
        {
            var _productoQuery = _productoRepository.Query();
            int total = _productoQuery.Count();
            return total;
        }
        private Dictionary<string, int> VentasUltimaSemana()
        {
            var resultado = new Dictionary<string, int>();
            var _ventaQuery = _ventaRepository.Query();
            if (_ventaQuery.Any())
            {
                var tablaVenta = ReturnSales(_ventaQuery, -7);
                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro!.Value.Date)
                    .OrderBy(o => o.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }
            return resultado;
        }
    }
}