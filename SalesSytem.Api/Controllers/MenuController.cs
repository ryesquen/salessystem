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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllByUserId(int id)
        {
            var response = new BaseResponse<List<MenuDto>>();
            try
            {
                var list = await _menuService.GetAllByUserId(id);
                if (list is not null)
                {
                    if (list.Count > 0)
                    {
                        response.Message = "consulta exitosa";
                        response.Data = list;
                    }
                    else
                    {
                        response.Message = "no se encontraron registros";
                        response.IsSuccess = false;
                    }
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
