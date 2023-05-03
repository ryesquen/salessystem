namespace SalesSystem.Application.DTOs
{
    public class SesionDto
    {
        public int IdUsuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get; set; }
        public string? RolNombre { get; set; }

        public static implicit operator List<object>(SesionDto v)
        {
            throw new NotImplementedException();
        }
    }
}