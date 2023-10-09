
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class VentaController : Controller
    {
        private readonly ILogger<VentaController> _logger;

        public VentaController(ILogger<VentaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("NuevaVenta")]
        public IActionResult NuevaVenta()
        {
            return View();
        }
        [HttpGet("HistorialVenta")]
        public IActionResult HistorialVenta()
        {
            return View();
        }

    }

}