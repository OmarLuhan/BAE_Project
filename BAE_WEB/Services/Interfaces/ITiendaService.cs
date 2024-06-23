using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface ITiendaService
    {
        Task<List<Tiendum>> Lista();
        Task<Tiendum> Crear(Tiendum entidad);
        Task<Tiendum> Editar(Tiendum entidad);
        Task<bool> Eliminar(int idTienda);
    }
}