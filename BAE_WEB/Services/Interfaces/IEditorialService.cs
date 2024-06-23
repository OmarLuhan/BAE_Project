using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface IEditorialService
    {
        Task<List<Editorial>> Lista();
        Task<Editorial> Crear(Editorial entidad);
        Task<Editorial> Editar(Editorial entidad);
        Task<bool> Eliminar(int idEditorial);
    }
}