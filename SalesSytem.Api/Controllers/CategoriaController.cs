using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Commons.Response;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;

namespace SalesSytem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new BaseResponse<List<CategoriaDto>>();
            try
            {
                var list = await _categoriaService.GetAll();
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
    }
}
