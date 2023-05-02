using System;
using System.Collections.Generic;

namespace SalesSystem.Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            MenuRoles = new HashSet<MenuRol>();
        }

        public int IdMenu { get; set; }
        public string? Nombre { get; set; }
        public string? Icono { get; set; }
        public string? Url { get; set; }

        public virtual ICollection<MenuRol> MenuRoles { get; set; }
    }
}
