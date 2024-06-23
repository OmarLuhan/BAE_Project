using BAE_WEB.Models.ViewModels;
using BAE_WEB.Services.Interfaces;
using BAE_WEB.Utils.CustomFilter;
using BAE_WEB.Utils.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BAE_WEB.Controllers
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
                VMDashboard vmDashboard = new()
                {
                    TotalVentas = await _dashBoardService.TotalVentasUltimaSemana(),
                    TotalIngresos = await _dashBoardService.TotalIngresosUltimaSemana(),
                    TotalLibros = await _dashBoardService.TotalLibros(),
                    PedidosRestantes = await _dashBoardService.PedidosRestantes()
                };

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