using SalesSystem.Application.DTOs;

namespace SalesSystem.Application.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> GetAll();
        Task<ProductoDto> Add(ProductoDto usuarioDto);
        Task<bool> Edit(ProductoDto usuarioDto);
        Task<bool> Delete(int id);
    }
}