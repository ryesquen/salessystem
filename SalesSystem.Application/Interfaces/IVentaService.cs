using SalesSystem.Application.DTOs;

namespace SalesSystem.Application.Interfaces
{
    public interface IVentaService
    {
        Task<VentaDto> Add(VentaDto dto);
        Task<List<VentaDto>> History(string findBy, string NumeroVenta, string fechaInicio, string fechaFin);
        Task<List<ReporteDto>> Report(string fechaInicio, string fechaFin);
    }
}