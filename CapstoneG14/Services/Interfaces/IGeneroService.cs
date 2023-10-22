
using CapstoneG14.Models;

namespace CapstoneG14.Services.Interfaces
{
    public interface IGeneroService
    {
        Task<List<Genero>> Lista();
        Task<Genero> Crear(Genero entidad);
        Task<Genero> Editar(Genero entidad);
        Task<bool> Eliminar(int idGenero);
    }
}