using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface IEditorialService
    {
        Task<List<Editorial>> Lista();
        Task<Editorial> Crear(Editorial entidad);
        Task<Editorial> Editar(Editorial entidad);
        Task<bool> Eliminar(int idEditorial);
    }
}