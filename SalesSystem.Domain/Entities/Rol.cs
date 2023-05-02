using System;
using System.Collections.Generic;

namespace SalesSystem.Domain.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            MenuRoles = new HashSet<MenuRol>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<MenuRol> MenuRoles { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
