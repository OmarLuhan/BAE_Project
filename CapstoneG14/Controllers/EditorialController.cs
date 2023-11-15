
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
    public class EditorialController : Controller
    {
        private readonly ILogger<EditorialController> _logger;
        private readonly IMapper _mapper;
        private readonly IEditorialService _editorialService;
        public EditorialController(ILogger<EditorialController> logger, IMapper mapper, IEditorialService editorialService)
        {
            _logger = logger;
            _mapper = mapper;
            _editorialService = editorialService;
        }
        [ClaimRequirement("Editorial", "Index")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<VMEditorial> vmTipo = _mapper.Map<List<VMEditorial>>(await _editorialService.Lista());
            _logger.LogInformation("Listando tipos de productos");
            return StatusCode(StatusCodes.Status200OK, new { data = vmTipo });
        }
        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] VMEditorial modelo)
        {
            GenericResponse<VMEditorial> gResponse = new GenericResponse<VMEditorial>();
            try
            {
                Editorial editorial_creado = await _editorialService.Crear(_mapper.Map<Editorial>(modelo));
                modelo = _mapper.Map<VMEditorial>(editorial_creado);
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
        public async Task<IActionResult> Editar([FromBody] VMEditorial modelo)
        {
            GenericResponse<VMEditorial> gResponse = new();
            try
            {
                Editorial editorial_editado = await _editorialService.Editar(_mapper.Map<Editorial>(modelo));
                modelo = _mapper.Map<VMEditorial>(editorial_editado);
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
        public async Task<IActionResult> Eliminar(int idEditorial)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _editorialService.Eliminar(idEditorial);
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