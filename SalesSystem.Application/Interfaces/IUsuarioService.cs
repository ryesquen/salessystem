using SalesSystem.Application.DTOs;

namespace SalesSystem.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> GetAll();
        Task<SesionDto> Valid(string correo, string clave);
        Task<UsuarioDto> Add(UsuarioDto usuarioDto);
        Task<bool> Edit(UsuarioDto usuarioDto);
        Task<bool> Delete(int id);
    }
}
