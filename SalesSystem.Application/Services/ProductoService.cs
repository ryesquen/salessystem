using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;
using System.Globalization;

namespace SalesSystem.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _repository;
        private readonly IMapper _mapper;
        public ProductoService(IGenericRepository<Producto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ProductoDto>> GetAll()
        {
            try
            {
                var productos = await _repository.Query().Include(r => r.IdCategoriaNavigation).ToListAsync();
                return _mapper.Map<List<ProductoDto>>(productos);
            }
            catch { throw; }
        }
        public async Task<ProductoDto> Add(ProductoDto productoDto)
        {
            try
            {
                var producto = await _repository.Add(_mapper.Map<Producto>(productoDto));
                if (producto.IdProducto == 0) throw new TaskCanceledException("El producto no se pudo crear");
                return _mapper.Map<ProductoDto>(producto);
            }
            catch { throw; }
        }
        public async Task<bool> Edit(ProductoDto dto)
        {
            try
            {
                var entity = await _repository.Get(u => u.IdProducto == dto.IdProducto) ?? throw new TaskCanceledException("El producto no existe");
                entity.Stock = dto.Stock;
                entity.Nombre = dto.Nombre;
                entity.IdCategoria = dto.IdCategoria;
                entity.Stock = dto.Stock;
                entity.Precio = Convert.ToDecimal(dto.Precio, new CultureInfo("es-PE"));
                var result = await _repository.Edit(entity);
                if (!result) throw new TaskCanceledException("No se pudo editar");
                return result;
            }
            catch { throw; }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _repository.Get(u => u.IdProducto == id) ?? throw new TaskCanceledException("El producto no existe");
                var result = await _repository.Delete(entity);
                if (!result) throw new TaskCanceledException("No se pudo eliminar");
                return result;
            }
            catch { throw; }
        }

    }
}