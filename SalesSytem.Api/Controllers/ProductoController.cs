using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Commons.Response;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;

namespace SalesSytem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new BaseResponse<List<ProductoDto>>();
            try
            {
                var list = await _productoService.GetAll();
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
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductoDto dto)
        {
            var response = new BaseResponse<ProductoDto>();
            try
            {
                var obj = await _productoService.Add(dto);
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
        public async Task<IActionResult> Edit(int id, [FromBody] ProductoDto dto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                dto.IdProducto = id;
                var obj = await _productoService.Edit(dto);
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
                var obj = await _productoService.Delete(id);
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
