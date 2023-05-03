using AutoMapper;
using SalesSystem.Application.DTOs;
using SalesSystem.Domain.Entities;
using System.Globalization;

namespace SalesSystem.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(
                    d => d.RolNombre,
                    opt => opt.MapFrom(
                        o => o.IdRolNavigation!.Nombre)
                )
                .ForMember(
                d => d.EsActivo,
                opt => opt.MapFrom(
                    o => Convert.ToBoolean(o.EsActivo) ? 1 : 0
                    )
                );
            CreateMap<Usuario, SesionDto>()
                .ForMember(
                    d => d.RolNombre,
                    opt => opt.MapFrom(
                        o => o.IdRolNavigation!.Nombre)
                );
            CreateMap<UsuarioDto, Usuario>()
                .ForMember(
                    d => d.IdRolNavigation,
                    opt => opt.Ignore())
                .ForMember(
                    d => d.EsActivo,
                    opt => opt.MapFrom(
                        o => o.EsActivo == 1
                        )
                );
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>()
                .ForMember(
                    d => d.CategoriaNombre,
                    opt => opt.MapFrom(
                        o => o.IdCategoriaNavigation!.Nombre)
                )
                .ForMember(
                d => d.EsActivo,
                opt => opt.MapFrom(
                    o => Convert.ToBoolean(o.EsActivo) ? 1 : 0
                    )
                )
                .ForMember(
                d => d.Precio,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.Precio!.Value, new CultureInfo("es-PE"))
                    )
                );
            CreateMap<ProductoDto, Producto>()
                .ForMember(
                    d => d.IdCategoriaNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(
                d => d.EsActivo,
                opt => opt.MapFrom(
                    o => o.EsActivo == 1
                    )
                )
                .ForMember(
                d => d.Precio,
                opt => opt.MapFrom(
                    o => Convert.ToDecimal(o.Precio, new CultureInfo("es-PE"))
                    )
                );
            CreateMap<Venta, VentaDto>()
                .ForMember(
                d => d.Total,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.Total!.Value, new CultureInfo("es-PE"))
                    )
                )
                .ForMember(
                d => d.FechaRegistro,
                opt => opt.MapFrom(
                    o => o.FechaRegistro!.Value.ToString("dd/MM/yyyy")
                    )
                );
            CreateMap<VentaDto, Venta>()
                .ForMember(
                d => d.Total,
                opt => opt.MapFrom(
                    o => Convert.ToDecimal(o.Total, new CultureInfo("es-PE"))
                    )
                )
                .ForMember(
                d => d.FechaRegistro,
                opt => opt.MapFrom(
                    o => Convert.ToDateTime(o.FechaRegistro)
                    )
                );
            CreateMap<DetalleVenta, DetalleVentaDto>()
                .ForMember(
                    d => d.ProductoNombre,
                    opt => opt.MapFrom(
                        o => o.IdProductoNavigation!.Nombre)
                )
                .ForMember(
                d => d.Precio,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.Precio!.Value, new CultureInfo("es-PE"))
                    )
                )
                .ForMember(
                d => d.Total,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.Total!.Value, new CultureInfo("es-PE"))
                    )
                );
            CreateMap<DetalleVentaDto, DetalleVenta>()
               .ForMember(
                   d => d.IdProductoNavigation,
                   opt => opt.Ignore()
               )
               .ForMember(
               d => d.Precio,
               opt => opt.MapFrom(
                   o => Convert.ToDecimal(o.Precio, new CultureInfo("es-PE"))
                   )
               )
               .ForMember(
               d => d.Total,
               opt => opt.MapFrom(
                   o => Convert.ToDecimal(o.Total, new CultureInfo("es-PE"))
                   )
               );
            CreateMap<DetalleVenta, ReporteDto>()
                .ForMember(
                d => d.FechaRegistro,
                opt => opt.MapFrom(
                    o => o.IdVentaNavigation!.FechaRegistro!.Value.ToString("dd/MM/yyyy")
                    )
                )
                .ForMember(
                d => d.NumeroDocumento,
                opt => opt.MapFrom(
                    o => o.IdVentaNavigation!.NumeroDocumento
                    )
                )
                .ForMember(
                d => d.TipoPago,
                opt => opt.MapFrom(
                    o => o.IdVentaNavigation!.TipoPago
                    )
                )
                .ForMember(
                d => d.TotalVenta,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.IdVentaNavigation!.Total!.Value, new CultureInfo("es-PE"))
                    )
                )
                .ForMember(
                d => d.Producto,
                opt => opt.MapFrom(
                    o => o.IdProductoNavigation!.Nombre
                    )
                )
                .ForMember(
                d => d.Precio,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.Precio!.Value, new CultureInfo("es-PE"))
                    )
                )
                .ForMember(
                d => d.Total,
                opt => opt.MapFrom(
                    o => Convert.ToString(o.Total!.Value, new CultureInfo("es-PE"))
                    )
                );
        }
    }
}