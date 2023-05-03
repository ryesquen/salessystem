namespace SalesSystem.Application.DTOs
{
    public class DashBoardDto
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public List<VentaSemanaDto>? VentasUltimaSemana { get; set; }
    }
}
