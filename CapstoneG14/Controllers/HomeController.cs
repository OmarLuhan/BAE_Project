
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneG14.Models;
using CapstoneG14.Utilities.Response;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;

namespace CapstoneG14.Controllers;
[Authorize]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly IMapper _mapper;
    public HomeController(IUsuarioService usuarioService, IMapper mapper)
    {
        _usuarioService = usuarioService;
        _mapper = mapper;
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("NoAutorizado")]
    public IActionResult NoAutorizado()
    {
        return View();
    }
    [HttpGet("Perfil")]
    public IActionResult Perfil()
    {
        return View();
    }

    [HttpGet("Salir")]
    public async Task<IActionResult> Salir()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Acceso");
    }

    [HttpGet("ObtenerUsuario")]
    public async Task<IActionResult> ObtenerUsuario()
    {
        GenericResponse<VMUsuario> response = new();
        try
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .SingleOrDefault();
            VMUsuario usuario = _mapper.Map<VMUsuario>(await _usuarioService.ObtenerPorId(int.Parse(idUsuario)));
            response.Estado = true;
            response.Objeto = usuario;
        }
        catch (Exception ex)
        {
            response.Estado = false;
            response.Mensaje = ex.Message;
        }
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost("GuardarPerfil")]
    public async Task<IActionResult> GuardarPerfil([FromBody] VMUsuario modelo)
    {
        GenericResponse<VMUsuario> response = new();
        try
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .SingleOrDefault();
            Usuario entidad = _mapper.Map<Usuario>(modelo);
            entidad.IdUsuario = int.Parse(idUsuario);
            bool resultado = await _usuarioService.GuardarPerfil(entidad);
            response.Estado = resultado;
        }
        catch (Exception ex)
        {
            response.Estado = false;
            response.Mensaje = ex.Message;
        }
        return StatusCode(StatusCodes.Status200OK, response);
    }

    [HttpPost("CambiarClave")]
    public async Task<IActionResult> CambiarClave([FromBody] VMCambiarClave modelo)
    {
        GenericResponse<bool> response = new();
        try
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .SingleOrDefault();

            bool resultado = await _usuarioService.CambiarClave(int.Parse(idUsuario), modelo.ClaveActual, modelo.ClaveNueva);
            response.Estado = resultado;
        }
        catch (Exception ex)
        {
            response.Estado = false;
            response.Mensaje = ex.Message;
        }
        return StatusCode(StatusCodes.Status200OK, response);
    }

}
