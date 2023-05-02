using System;
using System.Collections.Generic;

namespace SalesSystem.Domain.Entities
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public int NumeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
