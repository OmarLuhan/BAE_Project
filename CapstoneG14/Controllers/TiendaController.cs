
using AutoMapper;
using CapstoneG14.Models;
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
    public class TiendaController : Controller
    {
        private readonly ILogger<TiendaController> _logger;
        private readonly IMapper _mapper;
        private readonly ITiendaService _tiendaService;
        public TiendaController(ILogger<TiendaController> logger, IMapper mapper, ITiendaService tiendaService)
        {
            _logger = logger;
            _mapper = mapper;
            _tiendaService = tiendaService;
        }

        [ClaimRequirement("Tienda", "Index")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<VMTienda> vmTienda = _mapper.Map<List<VMTienda>>(await _tiendaService.Lista());
            _logger.LogInformation("Listando tiendas");
            return StatusCode(StatusCodes.Status200OK, new { data = vmTienda });
        }
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] VMTienda modelo)
        {
            GenericResponse<VMTienda> gResponse = new();
            try
            {
                Tiendum tienda_creada = await _tiendaService.Crear(_mapper.Map<Tiendum>(modelo));
                modelo = _mapper.Map<VMTienda>(tienda_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] VMTienda modelo)
        {
            GenericResponse<VMTienda> gResponse = new();
            try
            {
                Tiendum tienda_editada = await _tiendaService.Editar(_mapper.Map<Tiendum>(modelo));
                modelo = _mapper.Map<VMTienda>(tienda_editada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eiminar(int idTienda)
        {
            GenericResponse<VMTienda> gResponse = new();
            try
            {
                gResponse.Estado = await _tiendaService.Eliminar(idTienda);
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