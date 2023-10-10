using Microsoft.EntityFrameworkCore;
using CapstoneG14.Models;
using CapstoneG14.Services.Interfaces;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class LibroService : ILibroService
    {
        private readonly IGenericRepository<Libro> _libroRepository;
        private readonly IFirebaseService _firebaseService;
        public LibroService(IGenericRepository<Libro> libroRepository, IFirebaseService fireBaseService)
        {
            _libroRepository = libroRepository;
            _firebaseService = fireBaseService;
        }
        public async Task<Libro> Crear(Libro entidad, Stream? imagen = null, string NombreImagen = "")
        {
            Libro? libro_existe = await _libroRepository.Obtener(l => l.CodigoBarra == entidad.CodigoBarra && l.Isbn == entidad.Isbn);
            if (libro_existe != null)
                throw new TaskCanceledException("El codigo de barras o el isbn ya existe");
            try
            {
                entidad.NombreImagen = NombreImagen;
                if (imagen != null)
                {
                    string url_imagen = await _firebaseService.SubirStorage(imagen, "carpeta_producto", NombreImagen);
                    entidad.UrlImagen = url_imagen;
                }
                Libro libro_creado = await _libroRepository.Create(entidad);
                if (libro_creado.IdLibro == 0)
                    throw new TaskCanceledException("No se pudo crear el libro");
                IQueryable<Libro> query = await _libroRepository.Consultar(l => l.IdLibro == libro_creado.IdLibro);
                libro_creado = query.Include(e => e.IdEditorialNavigation)
                                    .Include(g => g.IdGeneroNavigation).First();
                return libro_creado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Libro> Editar(Libro entidad, Stream? imagen = null, string NombreImagen = "")
        {
            Libro? libro_existe = await _libroRepository.Obtener(l => l.CodigoBarra == entidad.CodigoBarra && l.IdLibro != entidad.IdLibro);
            if (libro_existe != null)
                throw new TaskCanceledException("El codigo de barras ya existe");
            try
            {
                IQueryable<Libro>? query_libro = await _libroRepository.Consultar(l => l.IdLibro == entidad.IdLibro);
                Libro? libro_editar = query_libro.First();
                libro_editar.CodigoBarra = entidad.CodigoBarra;
                libro_editar.Isbn = entidad.Isbn;
                libro_editar.Autor = entidad.Autor;
                libro_editar.Titulo = entidad.Titulo;
                libro_editar.IdEditorial = entidad.IdEditorial;
                libro_editar.IdGenero = entidad.IdGenero;
                libro_editar.Pendientes = entidad.Pendientes;
                libro_editar.Precio = entidad.Precio;
                libro_editar.EsActivo = entidad.EsActivo;
                if (libro_editar.NombreImagen == "" || libro_editar.NombreImagen == null)
                    libro_editar.NombreImagen = NombreImagen;
                if (imagen != null)
                {
                    string url_imagen = await _firebaseService.SubirStorage(imagen, "carpeta_producto", libro_editar.NombreImagen);
                    libro_editar.UrlImagen = url_imagen;
                }
                bool respuesta = await _libroRepository.Update(libro_editar);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el libro");
                Libro libro_editado = query_libro.Include(e => e.IdEditorialNavigation)
                                                 .Include(g => g.IdGeneroNavigation).First();
                return libro_editado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idLibro)
        {
            try
            {
                Libro libro_encotrado = await _libroRepository.Obtener(l => l.IdLibro == idLibro) ?? throw new TaskCanceledException("No se encontro el libro");
                string? nombre_imagen = libro_encotrado.NombreImagen;
                bool respuesta = await _libroRepository.Delete(libro_encotrado);
                if (respuesta)
                    await _firebaseService.EliminarStorage("carpeta_producto", nombre_imagen);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Libro>> Lista()
        {
            IQueryable<Libro> query = await _libroRepository.Consultar();
            return query.Include(e => e.IdEditorialNavigation)
                        .Include(g => g.IdGeneroNavigation).ToList();
        }
    }
}