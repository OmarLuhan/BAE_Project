
using CapstoneG14.Models;
using CapstoneG14.Repositories.Interfaces;
using CapstoneG14.Services.Interfaces;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class TiendaService : ITiendaService
    {
        private readonly IGenericRepository<Tiendum> _repository;
        public TiendaService(IGenericRepository<Tiendum> repository)
        {
            _repository = repository;
        }
        public async Task<Tiendum> Crear(Tiendum entidad)
        {
            try
            {
                Tiendum tienda_creada = await _repository.Create(entidad);
                if (tienda_creada.IdTienda == 0)
                    throw new TaskCanceledException("no se pudo crear la tienda");
                return tienda_creada;
            }
            catch { throw; }
        }

        public async Task<Tiendum> Editar(Tiendum entidad)
        {
            try
            {
                Tiendum tienda_encontrada = await _repository.Obtener(t => t.IdTienda == entidad.IdTienda);
                tienda_encontrada.Descripcion = entidad.Descripcion;
                tienda_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _repository.Update(tienda_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("no se pudo editar la tienda");
                return tienda_encontrada;
            }
            catch { throw; }
        }

        public async Task<bool> Eliminar(int idTienda)
        {
            try
            {
                Tiendum tienda_encontrada = await _repository.Obtener(t => t.IdTienda == idTienda) ?? throw new TaskCanceledException("La tienda no existe");
                bool respuesta = await _repository.Delete(tienda_encontrada);
                return respuesta;
            }
            catch { throw; }
        }

        public async Task<List<Tiendum>> Lista()
        {
            try
            {
                IQueryable<Tiendum> query = await _repository.Consultar();
                return query.ToList();
            }
            catch { throw; }
        }
    }
}