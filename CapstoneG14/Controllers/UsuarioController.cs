
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CapstoneG14.Models;
using CapstoneG14.Utilities.Response;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;
using CapstoneG14.Utilities.CustomFilter;

namespace CapstoneG14.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolService;
        public UsuarioController(IRolService rolService, IMapper mapper, IUsuarioService usuarioService)
        {

            _rolService = rolService;
            _mapper = mapper;
            _usuarioService = usuarioService;

        }
        [ClaimRequirement("Usuario", "Index")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ListaRoles")]
        public async Task<IActionResult> ListaRoles()
        {
            var lista = await _rolService.Lista();
            List<VMRol> vmListaRoles = _mapper.Map<List<VMRol>>(lista);
            return StatusCode(StatusCodes.Status200OK, vmListaRoles);
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<VMUsuario> vmUsuarioLista = _mapper.Map<List<VMUsuario>>(await _usuarioService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmUsuarioLista });
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMUsuario> gresponse = new GenericResponse<VMUsuario>();
            try
            {
                VMUsuario? vmUsuario = JsonConvert.DeserializeObject<VMUsuario>(modelo);
                string nombreFoto = "";
                Stream? fotoStream = null;
                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extencion = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombre_en_codigo, extencion);
                    fotoStream = foto.OpenReadStream();
                }
                string urlPlantilaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/EnviarClave?correo=[correo]&clave=[clave]";
                Usuario usuario_creado = await _usuarioService.Crear(_mapper.Map<Usuario>(vmUsuario), fotoStream, nombreFoto, urlPlantilaCorreo);
                vmUsuario = _mapper.Map<VMUsuario>(usuario_creado);
                gresponse.Estado = true;
                gresponse.Objeto = vmUsuario;
            }
            catch (Exception ex)
            {
                gresponse.Estado = false;
                gresponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gresponse);

        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromForm] IFormFile foto, [FromForm] string modelo)
        {
            GenericResponse<VMUsuario> gresponse = new GenericResponse<VMUsuario>();
            try
            {
                VMUsuario? vmUsuario = JsonConvert.DeserializeObject<VMUsuario>(modelo);
                string nombreFoto = "";
                Stream fotoStream = null;
                if (foto != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extencion = Path.GetExtension(foto.FileName);
                    nombreFoto = string.Concat(nombre_en_codigo, extencion);
                    fotoStream = foto.OpenReadStream();
                }
                Usuario usuario_Editado = await _usuarioService.Editar(_mapper.Map<Usuario>(vmUsuario), fotoStream, nombreFoto);
                vmUsuario = _mapper.Map<VMUsuario>(usuario_Editado);
                gresponse.Estado = true;
                gresponse.Objeto = vmUsuario;
            }
            catch (Exception ex)
            {
                gresponse.Estado = false;
                gresponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gresponse);

        }
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(int idUsuario)
        {
            GenericResponse<string> gresponse = new GenericResponse<string>();
            try
            {
                gresponse.Estado = await _usuarioService.Eliminar(idUsuario);

            }
            catch (Exception ex)
            {
                gresponse.Estado = false;
                gresponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gresponse);

        }


    }

}