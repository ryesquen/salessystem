using SalesSystem.Application.DTOs;

namespace SalesSystem.Application.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDto>> GetAllByUserId(int id);
    }
}