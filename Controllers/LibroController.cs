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
    public class LibroController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILibroService _libroService;
        public LibroController(IMapper mapper, ILibroService libroService)
        {
            _mapper = mapper;
            _libroService = libroService;

        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<VMLibro> vmLibroLista = _mapper.Map<List<VMLibro>>(await _libroService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmLibroLista });
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<VMLibro> gResponse = new GenericResponse<VMLibro>();
            try
            {
                VMLibro? vmLibro = JsonConvert.DeserializeObject<VMLibro>(modelo);
                string nombreImagen = "";
                Stream imagenStream = null;
                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }
                Libro libro_creado = await _libroService.Crear(_mapper.Map<Libro>(vmLibro), imagenStream, nombreImagen);
                vmLibro = _mapper.Map<VMLibro>(libro_creado);
                gResponse.Estado = true;
                gResponse.Objeto = vmLibro;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<VMLibro> gResponse = new GenericResponse<VMLibro>();
            try
            {
                VMLibro? vmLibro = JsonConvert.DeserializeObject<VMLibro>(modelo);
                string nombreImagen = "";
                Stream imagenStream = null;
                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }
                Libro libro_editado = await _libroService.Editar(_mapper.Map<Libro>(vmLibro), imagenStream, nombreImagen);
                vmLibro = _mapper.Map<VMLibro>(libro_editado);
                gResponse.Estado = true;
                gResponse.Objeto = vmLibro;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(int idLibro)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _libroService.Eliminar(idLibro);
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
