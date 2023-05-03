using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Commons.Response;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;
using SalesSystem.Application.Services;

namespace SalesSytem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new BaseResponse<List<UsuarioDto>>();
            try
            {
                var list = await _usuarioService.GetAll();
                if (list.Any())
                {
                    response.Data = list;
                    response.Message = "consulta exitosa";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Valid([FromBody] LoginDto dto)
        {
            var response = new BaseResponse<SesionDto>();
            try
            {
                var obj = await _usuarioService.Valid(dto.Correo!, dto.Clave!);
                if (obj is not null)
                {
                    response.Data = obj;
                    response.Message = "validación exitosa";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UsuarioDto dto)
        {
            var response = new BaseResponse<UsuarioDto>();
            try
            {
                var obj = await _usuarioService.Add(dto);
                if (obj is not null)
                {
                    response.Data = obj;
                    response.Message = "registro exitoso";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Created("", response.Data);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UsuarioDto dto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                dto.IdUsuario = id;
                var obj = await _usuarioService.Edit(dto);
                if (obj)
                {
                    response.Data = obj;
                    response.Message = "actualización exitosa";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var obj = await _usuarioService.Delete(id);
                if (obj)
                {
                    response.Data = obj;
                    response.Message = "eliminación exitosa";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
