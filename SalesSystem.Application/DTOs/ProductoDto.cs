﻿namespace SalesSystem.Application.DTOs
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public int? IdCategoria { get; set; }
        public string? CategoriaNombre { get; set; }
        public int? Stock { get; set; }
        public string? Precio { get; set; }
        public int? EsActivo { get; set; }
    }
}