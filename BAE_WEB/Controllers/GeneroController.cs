using AutoMapper;
using BAE_WEB.Models;
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
    public class GeneroController : Controller
    {
        private readonly ILogger<GeneroController> _logger;
        private readonly IMapper _mapper;
        private readonly IGeneroService _generoService;

        public GeneroController(ILogger<GeneroController> logger, IMapper mapper, IGeneroService generoService)
        {
            _logger = logger;
            _mapper = mapper;
            _generoService = generoService;
        }
        [ClaimRequirement("Genero", "Index")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<VMGenero> vmGenero = _mapper.Map<List<VMGenero>>(await _generoService.Lista());
            _logger.LogInformation("Listando tipos de productos");
            return StatusCode(StatusCodes.Status200OK, new { data = vmGenero });
        }
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] VMGenero modelo)
        {
            GenericResponse<VMGenero> gResponse = new GenericResponse<VMGenero>();
            try
            {
                Genero genero_creado = await _generoService.Crear(_mapper.Map<Genero>(modelo));
                modelo = _mapper.Map<VMGenero>(genero_creado);
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
        public async Task<IActionResult> Editar([FromBody] VMGenero modelo)
        {
            GenericResponse<VMGenero> gResponse = new();
            try
            {
                Genero genero_editado = await _generoService.Editar(_mapper.Map<Genero>(modelo));
                modelo = _mapper.Map<VMGenero>(genero_editado);
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
        public async Task<IActionResult> Eliminar(int idGenero)
        {
            GenericResponse<string> gResponse = new();
            try
            {
                gResponse.Estado = await _generoService.Eliminar(idGenero);
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