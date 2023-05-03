namespace SalesSystem.Application.DTOs
{
    public class VentaDto
    {
        public int IdVenta { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public string? Total { get; set; }
        public string? FechaRegistro { get; set; }
        public virtual ICollection<DetalleVentaDto>? DetalleVentasDto { get; set; }
    }
}