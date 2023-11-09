
using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface ITiendaService
    {
        Task<List<Tiendum>> Lista();
        Task<Tiendum> Crear(Tiendum entidad);
        Task<Tiendum> Editar(Tiendum entidad);
        Task<bool> Eliminar(int idTienda);
    }
}