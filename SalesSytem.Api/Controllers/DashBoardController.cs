using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Commons.Response;
using SalesSystem.Application.DTOs;
using SalesSystem.Application.Interfaces;

namespace SalesSytem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }
        [HttpGet]
        public async Task<IActionResult> Resume()
        {
            var response = new BaseResponse<DashBoardDto>();
            try
            {
                var obj = _dashBoardService.Resume();
                if (obj is not null)
                {
                    response.Data = obj;
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