using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
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