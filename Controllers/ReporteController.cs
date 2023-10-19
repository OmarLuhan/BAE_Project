
using AutoMapper;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;
using CapstoneG14.Utilities.CustomFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ReporteController : Controller
    {
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;
        public ReporteController(IVentaService ventaService, IMapper mapper)
        {
            _ventaService = ventaService;
            _mapper = mapper;
        }
        [ClaimRequirement("Reporte", "Index")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ReporteVenta")]
        public async Task<IActionResult> ReporteVenta(string fechaInicio, string fechaFin)
        {
            List<VMReporteVenta> vmLista = _mapper.Map<List<VMReporteVenta>>(await _ventaService.Reporte(fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }
    }
}