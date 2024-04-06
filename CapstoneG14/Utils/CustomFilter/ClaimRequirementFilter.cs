using System.Security.Claims;
using CapstoneG14.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CapstoneG14.Utilities.CustomFilter
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private string _controller;
        private string _action;
        private IMenuService _menuService;
        public ClaimRequirementFilter(string controller, string action, IMenuService menuService)
        {
            _controller = controller;
            _action = action;
            _menuService = menuService;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            ClaimsPrincipal claimUser = context.HttpContext.User;
            string idUsuario = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value)
            .SingleOrDefault();
            bool permiso = await _menuService.PermisoMenu(int.Parse(idUsuario), _controller, _action);
            if (!permiso)
            {
                context.Result = new RedirectToActionResult("NoAutorizado", "Home", null);
            }
        }
    }
}