
using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<Menu>> ObtenerMenus(int idUsuario);
        Task<bool> PermisoMenu(int idUsuario, string controlador, string accion);

    }
}