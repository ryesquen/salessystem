using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;

namespace SalesSystem.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository, IGenericRepository<Usuario> usuarioRepository, IMapper mapper)
        {
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDto>> GetByUserId(int id)
        {
            var queryUsuario = _usuarioRepository.Query(u => u.IdUsuario == id);
            var queryMenuRol = _menuRolRepository.Query();
            var queryMenu = _menuRepository.Query();
            try
            {
                var result = queryUsuario
                            .Join(
                                queryMenuRol,
                                u => u.IdRol,
                                mr => mr.IdRolNavigation!.IdRol,
                                (u, mr) => mr
                            )
                            .Join(
                                queryMenu,
                                u => u.IdMenu,
                                m => m.IdMenu,
                                (u, m) => m
                            );
                var listaMenus = await result.ToListAsync();
                return _mapper.Map<List<MenuDto>>(listaMenus);
            }
            catch { throw; }
        }
    }
}