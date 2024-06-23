using BAE_WEB.Models;

namespace BAE_WEB.Services.Interfaces
{
    public interface IGeneroService
    {
        Task<List<Genero>> Lista();
        Task<Genero> Crear(Genero entidad);
        Task<Genero> Editar(Genero entidad);
        Task<bool> Eliminar(int idGenero);
    }
}