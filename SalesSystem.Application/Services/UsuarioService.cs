using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Domain.Entities;
using SalesSystem.Persistence.Interfaces;

namespace SalesSystem.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IMapper _mapper;
        public UsuarioService(IMapper mapper, IGenericRepository<Usuario> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<UsuarioDto>> GetAll()
        {
            try
            {
                var usuarios = await _repository.Query().Include(r => r.IdRolNavigation).ToListAsync();
                return _mapper.Map<List<UsuarioDto>>(usuarios);
            }
            catch { throw; }
        }
        public async Task<SesionDto> Valid(string correo, string clave)
        {
            try
            {
                var usuarioQuery = _repository.Query(u => u.Correo == correo && u.Clave == clave);
                if (await usuarioQuery.FirstOrDefaultAsync() is null) throw new TaskCanceledException("El usuario no existe");
                return _mapper.Map<SesionDto>(await usuarioQuery.Include(r => r.IdRolNavigation).FirstOrDefaultAsync());
            }
            catch { throw; }
        }
        public async Task<UsuarioDto> Add(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = await _repository.Add(_mapper.Map<Usuario>(usuarioDto));
                if (usuario.IdUsuario == 0) throw new TaskCanceledException("El usuario no se pudo crear");
                return _mapper.Map<UsuarioDto>(usuario);
            }
            catch { throw; }
        }
        public async Task<bool> Edit(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = await _repository.Get(u => u.IdUsuario == usuarioDto.IdUsuario) ?? throw new TaskCanceledException("El usuario no existe");
                usuario.NombreCompleto = usuarioDto.NombreCompleto;
                usuario.Correo = usuarioDto.Correo;
                usuario.Clave = usuarioDto.Clave;
                usuario.EsActivo = usuarioDto.EsActivo == 1;
                usuario.IdRol = usuarioDto.IdRol;
                var result = await _repository.Edit(usuario);
                if (!result) throw new TaskCanceledException("No se pudo editar");
                return result;
            }
            catch { throw; }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var usuario = await _repository.Get(u => u.IdUsuario == id) ?? throw new TaskCanceledException("El usuario no existe");
                var result = await _repository.Delete(usuario);
                if (!result) throw new TaskCanceledException("No se pudo eliminar");
                return result;
            }
            catch { throw; }
        }
    }
}