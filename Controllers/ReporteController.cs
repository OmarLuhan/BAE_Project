
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ReporteController : Controller
    {
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(ILogger<ReporteController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}