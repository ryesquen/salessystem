using SalesSystem.Application.DTOs;

namespace SalesSystem.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> GetAll();
    }
}