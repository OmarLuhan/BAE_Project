using BAE_WEB.Models;
using BAE_WEB.Services.Interfaces;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Services.Implementations
{
    public class GeneroService : IGeneroService
    {
        private readonly IGenericRepository<Genero> _generoRepository;
        public GeneroService(IGenericRepository<Genero> generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<Genero> Crear(Genero entidad)
        {
            try
            {
                Genero genero_creado = await _generoRepository.Create(entidad);
                if (genero_creado.IdGenero == 0)
                    throw new TaskCanceledException("no se pudo crear el genero");
                return genero_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Genero> Editar(Genero entidad)
        {
            try
            {
                Genero genero_encontrado = await _generoRepository.Obtener(g => g.IdGenero == entidad.IdGenero);
                genero_encontrado.Descripcion = entidad.Descripcion;
                genero_encontrado.EsActivo = entidad.EsActivo;
                bool respuesta = await _generoRepository.Update(genero_encontrado);
                if (!respuesta)
                    throw new TaskCanceledException("no se pudo editar el genero");
                return genero_encontrado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idGenero)
        {
            try
            {
                Genero genero_encontaado = await _generoRepository.Obtener(g => g.IdGenero == idGenero) ?? throw new TaskCanceledException("El genero no existe");
                bool respuesta = await _generoRepository.Delete(genero_encontaado);
                return respuesta;

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Genero>> Lista()
        {
            try
            {
                IQueryable<Genero> query = await _generoRepository.Consultar();
                return query.ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}