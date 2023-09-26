
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CapstoneG14.Models;
using CapstoneG14.Utilities.Response;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;

namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class NegocioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;
        public NegocioController(IMapper mapper, INegocioService negocioService)
        {
            _mapper = mapper;
            _negocioService = negocioService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Obtener")]
        public async Task<IActionResult> Obtener()
        {
            GenericResponse<VMNegocio> gRespoose = new GenericResponse<VMNegocio>();
            try
            {
                VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioService.Obtener());
                gRespoose.Estado = true;
                gRespoose.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                gRespoose.Estado = false;
                gRespoose.Mensaje = ex.Message;

            }
            return StatusCode(StatusCodes.Status200OK, gRespoose);
        }
        [HttpPost("GuardarCambios")]
        public async Task<IActionResult> GuardarCambios([FromForm] IFormFile logo, [FromForm] string modelo)
        {
            GenericResponse<VMNegocio> gRespoose = new GenericResponse<VMNegocio>();
            try
            {
                VMNegocio? vmNegocio = JsonConvert.DeserializeObject<VMNegocio>(modelo);
                string? nombreLogo = "";
                Stream? logoStream = null;
                if (logo != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extencion = Path.GetExtension(logo.FileName);
                    nombreLogo = string.Concat(nombre_en_codigo, extencion);
                    logoStream = logo.OpenReadStream();
                }
                Negocio negocio_editado = await _negocioService.GuardarCambios(_mapper.Map<Negocio>(vmNegocio)
                    , logoStream, nombreLogo);
                vmNegocio = _mapper.Map<VMNegocio>(negocio_editado);
                gRespoose.Estado = true;
                gRespoose.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                gRespoose.Estado = false;
                gRespoose.Mensaje = ex.Message;

            }
            return StatusCode(StatusCodes.Status200OK, gRespoose);
        }
    }
}
