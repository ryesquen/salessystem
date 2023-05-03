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
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VentaDto dto)
        {
            var response = new BaseResponse<VentaDto>();
            try
            {
                var obj = await _ventaService.Add(dto);
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
        [HttpGet("history")]
        public async Task<IActionResult> History(string? findBy, string? NumeroVenta = "", string? fechaInicio = "", string? fechaFin = "")
        {
            var response = new BaseResponse<List<VentaDto>>();
            try
            {
                var list = await _ventaService.History(findBy!, NumeroVenta!, fechaInicio!, fechaFin!);
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
        [HttpGet("report")]
        public async Task<IActionResult> Report(string? fechaInicio = "", string? fechaFin = "")
        {
            var response = new BaseResponse<List<ReporteDto>>();
            try
            {
                var list = await _ventaService.Report(fechaInicio!, fechaFin!);
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
