using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;
using System.Globalization;

namespace SalesSystem.Application.Services
{
    public class VentaService : IVentaService
    {
        private readonly IMapper _mapper;
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepository;
        public VentaService(IMapper mapper, IVentaRepository ventaRepository, IGenericRepository<DetalleVenta> detalleVentaRepository)
        {
            _mapper = mapper;
            _ventaRepository = ventaRepository;
            _detalleVentaRepository = detalleVentaRepository;
        }
        public async Task<VentaDto> Add(VentaDto dto)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.Register(_mapper.Map<Venta>(dto));
                if (ventaGenerada.IdVenta == 0) throw new TaskCanceledException("No se pudo crear");
                return _mapper.Map<VentaDto>(ventaGenerada);
            }
            catch { throw; }
        }

        public async Task<List<VentaDto>> History(string findBy, string NumeroVenta, string fechaInicio, string fechaFin)
        {
            var query = _ventaRepository.Query();
            var result = new List<Venta>();
            try
            {
                if (findBy.Equals("fecha"))
                {
                    var inicio = DateTime.ParseExact(fechaInicio, "ddMMyyyy", new CultureInfo("es-PE"));
                    var fin = DateTime.ParseExact(fechaFin, "ddMMyyyy", new CultureInfo("es-PE"));
                    result = await query.Where(v => v.FechaRegistro!.Value.Date >= inicio && v.FechaRegistro.Value.Date <= fin)
                        .Include(dv => dv.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }
                else
                {
                    result = await query.Where(v => v.NumeroDocumento == NumeroVenta)
                        .Include(dv => dv.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }
                return _mapper.Map<List<VentaDto>>(result);
            }
            catch { throw; }
        }
        public async Task<List<ReporteDto>> Report(string fechaInicio, string fechaFin)
        {
            var query = _detalleVentaRepository.Query();
            var result = new List<DetalleVenta>();
            try
            {

                var inicio = DateTime.ParseExact(fechaInicio, "ddMMyyyy", new CultureInfo("es-PE"));
                var fin = DateTime.ParseExact(fechaFin, "ddMMyyyy", new CultureInfo("es-PE"));
                result = await query.Where(v => v.IdVentaNavigation!.FechaRegistro!.Value.Date >= inicio && v.IdVentaNavigation.FechaRegistro.Value.Date <= fin)
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .ToListAsync();
                return _mapper.Map<List<ReporteDto>>(result);

            }
            catch { throw; }
        }
    }
}