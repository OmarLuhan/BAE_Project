using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;
using CapstoneG14.Utilities.CustomFilter;
using CapstoneG14.Utilities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashBoardService _dashBoardService;
        public DashboardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }
        [ClaimRequirement("Dashboard", "Index")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("ObtenerResumen")]
        public async Task<IActionResult> ObtenerResumen()
        {
            GenericResponse<VMDashboard> gResponse = new();

            try
            {
                VMDashboard vmDashboard = new VMDashboard();
                vmDashboard.TotalVentas = await _dashBoardService.TotalVentasUltimaSemana();
                vmDashboard.TotalIngresos = await _dashBoardService.TotalIngresosUltimaSemana();
                vmDashboard.TotalLibros = await _dashBoardService.TotalLibros();
                vmDashboard.PedidosRestantes = await _dashBoardService.PedidosRestantes();

                List<VMVentasSemana> listaVentaSemana = new();
                List<VMLibrosSemana> listaProductosSemana = new();

                foreach (KeyValuePair<string, int> item in await _dashBoardService.VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VMVentasSemana()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }
                foreach (KeyValuePair<string, int> item in await _dashBoardService.LibrosTopUltimaSemana())
                {
                    listaProductosSemana.Add(new VMLibrosSemana
                    {
                        Libro = item.Key,
                        Cantidad = item.Value
                    });
                }

                vmDashboard.VentasUltimaSemana = listaVentaSemana;
                vmDashboard.LibrosTopUltimaSemana = listaProductosSemana;

                gResponse.Estado = true;
                gResponse.Objeto = vmDashboard;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}