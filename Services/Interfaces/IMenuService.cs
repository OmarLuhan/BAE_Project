
using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<Menu>> ObtenerMenus(int idUsuario);
    }
}