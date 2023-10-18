
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using CapstoneG14.Models;
using CapstoneG14.Models.ViewModels;
using CapstoneG14.Services.Interfaces;

namespace CapstoneG14.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public AccesoController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        public async Task<IActionResult> Acceder(VMUsuarioLogin modelo)
        {
            Usuario usuario_encontrado = await _usuarioService.ObtenerPorCredenciales(modelo.Correo, modelo.Clave);
            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron Coincidencias";
                return View("Login");
            }
            ViewData["Mensaje"] = null;
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.Nombre),
                new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuario_encontrado.IdRol.ToString()),
                new Claim("UrlFoto", usuario_encontrado.UrlFoto)
            };
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authProperties = new()
            {
                AllowRefresh = true,
                IsPersistent = modelo.MantenerSesion
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("Index", "Dashboard");
        }


        public async Task<IActionResult> RestableceClave(VMUsuarioLogin modelo)
        {
            try
            {
                string urlPlantillaCorreo = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/RestablecerClavePlantilla?clave=[Clave]";
                if (modelo.Correo == null)
                {
                    ViewData["MensajeInfo"] = "Ingrese su correo electrónico";
                    ViewData["MensajeError"] = null;
                    ViewData["Mensaje"] = null;
                }
                else
                {
                    bool respuesta = await _usuarioService.RecuperarClave(modelo.Correo, urlPlantillaCorreo);

                    if (respuesta)
                    {
                        ViewData["Mensaje"] = "Se ha enviado un correo con las instrucciones para restablecer su clave";
                        ViewData["MensajeInfo"] = null;
                        ViewData["MensajeError"] = null;
                    }
                    else
                    {
                        ViewData["Mensaje"] = null;
                        ViewData["MensajeInfo"] = null;
                        ViewData["MensajeError"] = "Tenemos problemas para procesar su solicitud, por favor intente más tarde";
                    }
                }

            }
            catch (Exception ex)
            {
                ViewData["MensajeError"] = ex.Message;
                ViewData["Mensaje"] = null;
                ViewData["MensajeInfo"] = null;
            }
            return View();
        }
    }
}