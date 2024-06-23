using BAE_WEB.Models;
using BAE_WEB.Services.Interfaces;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Services.Implementations
{
    public class EditorialService : IEditorialService
    {
        private readonly IGenericRepository<Editorial> _editorialRepository;
        public EditorialService(IGenericRepository<Editorial> editorialRepository)
        {
            _editorialRepository = editorialRepository;
        }
        public async Task<Editorial> Crear(Editorial entidad)
        {
            try
            {
                Editorial editorial_creada = await _editorialRepository.Create(entidad);
                if (editorial_creada.IdEditorial == 0)
                    throw new TaskCanceledException("no se pudo crear la editorial");
                return editorial_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Editorial> Editar(Editorial entidad)
        {
            try
            {
                Editorial editorial_encontrada = await _editorialRepository.Obtener(e => e.IdEditorial == entidad.IdEditorial);
                editorial_encontrada.Descripcion = entidad.Descripcion;
                editorial_encontrada.EsActivo = entidad.EsActivo;
                bool respuesta = await _editorialRepository.Update(editorial_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("no se pudo editar la editorial");
                return editorial_encontrada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idEditorial)
        {
            try
            {
                Editorial editrial_encontrada = await _editorialRepository.Obtener(e => e.IdEditorial == idEditorial) ?? throw new TaskCanceledException("La editorial no existe");
                bool respuesta = await _editorialRepository.Delete(editrial_encontrada);
                if (!respuesta)
                    throw new TaskCanceledException("no se pudo eliminar la editorial");
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Editorial>> Lista()
        {
            try
            {
                IQueryable<Editorial> editoriales = await _editorialRepository.Consultar();
                return editoriales.ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}