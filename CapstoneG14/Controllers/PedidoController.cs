using CapstoneG14.Utilities.CustomFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;
        }
        [ClaimRequirement("Pedido", "NuevoPedido")]
        [HttpGet("NuevoPedido")]
        public IActionResult NuevoPedido()
        {
            return View();
        }
        [ClaimRequirement("pedido", "HistorialPedido")]
        [HttpGet("HistorialPedido")]
        public IActionResult HistorialPedido()
        {
            return View();
        }

    }
}