using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface ILibroService
    {
        Task<List<Libro>> Lista();
        Task<Libro> Crear(Libro entidad, Stream? imagen = null, string NombreImagen = "");
        Task<Libro> Editar(Libro entidad, Stream? imagen = null, string NombreImagen = "");
        Task<bool> Eliminar(int idLibro);
    }
}