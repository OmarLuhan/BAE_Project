
using CapstoneG14.Models;
using CapstoneG14.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CapstoneG14.Tests.RepositoryTest
{
    public class GenericRepositoryAddEntityTest
    {
        private BaeContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BaeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseCreate")
                .Options;
            return new BaeContext(options);
        }
        [Fact]
        public async Task Crear_usuario_vacio()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Usuario>(context);
            var entity = new Usuario();
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Crear_usuario()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Usuario>(context);
            var entity = new Usuario()
            {
                Nombre = "Juan",
                Correo = "juan@gmail.com",
                Telefono = "12345678",
                Clave = "dffr99fja94fa",
                IdRol = 1,
                NombreFoto = "foto.jpg",
                UrlFoto = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        public async Task Crear_usuario_sin_idRol()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Usuario>(context);
            var entity = new Usuario()
            {
                Nombre = "Juan",
                Correo = "juan@gmail.com",
                Telefono = "12345678",
                Clave = "dffr99fja94fa",
                NombreFoto = "foto.jpg",
                UrlFoto = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Crear_usuario_rol_4()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Usuario>(context);
            var entity = new Usuario()
            {
                Nombre = "Juan",
                Correo = "juan@gmail.com",
                Telefono = "12345678",
                Clave = "dffr99fja94fa",
                IdRol = 4,
                NombreFoto = "foto.jpg",
                UrlFoto = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Crear_libro_vacio()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Libro>(context);
            var entity = new Libro();
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        public async Task Crear_libro()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Libro>(context);
            var entity = new Libro()
            {
                Titulo = "El señor de los anillos",
                Autor = "J.R.R. Tolkien",
                IdEditorial = 4,
                IdGenero = 1,
                NombreImagen = "foto.jpg",
                UrlImagen = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Crear_libro_genero_Inexistente()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Libro>(context);
            var entity = new Libro()
            {
                Titulo = "El señor de los anillos",
                Autor = "J.R.R. Tolkien",
                IdEditorial = 4,
                IdGenero = 2000,
                NombreImagen = "foto.jpg",
                UrlImagen = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Crear_libro_editorial_Inexistente()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Libro>(context);
            var entity = new Libro()
            {
                Titulo = "El señor de los anillos",
                Autor = "J.R.R. Tolkien",
                IdEditorial = 2000,
                IdGenero = 1,
                NombreImagen = "foto.jpg",
                UrlImagen = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Crear_libro_editorial_genero_Inexistente()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Libro>(context);
            var entity = new Libro()
            {
                Titulo = "El señor de los anillos",
                Autor = "J.R.R. Tolkien",
                IdEditorial = 2000,
                IdGenero = 2000,
                NombreImagen = "foto.jpg",
                UrlImagen = "https://www.google.com",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta_vacia()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum();
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum()
            {
                IdUsuario = 1,
                Total = 100,
                NumeroVenta = "00021",
                FechaRegistro = DateTime.Now,
                DocumentoCliente = "12345678",
                NombreCliente = "Juan",
                SubTotal = 100,
                ImpuestoTotal = 0,
                IdTipoDocumentoVenta = 1,

            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta_sin_idTipoDocumentoVenta()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum()
            {
                IdUsuario = 1,
                Total = 100,
                NumeroVenta = "00021",
                FechaRegistro = DateTime.Now,
                DocumentoCliente = "12345678",
                NombreCliente = "Juan",
                SubTotal = 100,
                ImpuestoTotal = 0,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta_sin_idUsuario()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum()
            {
                Total = 100,
                NumeroVenta = "00021",
                FechaRegistro = DateTime.Now,
                DocumentoCliente = "12345678",
                NombreCliente = "Juan",
                SubTotal = 100,
                ImpuestoTotal = 0,
                IdTipoDocumentoVenta = 1,

            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta_sin_idUsuario_sin_idTipoDocumentoVenta()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum()
            {
                Total = 100,
                NumeroVenta = "00021",
                FechaRegistro = DateTime.Now,
                DocumentoCliente = "12345678",
                NombreCliente = "Juan",
                SubTotal = 100,
                ImpuestoTotal = 0,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta_sin_idUsuario_sin_idTipoDocumentoVenta_sin_nombreCliente()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum()
            {
                Total = 100,
                NumeroVenta = "00021",
                FechaRegistro = DateTime.Now,
                DocumentoCliente = "12345678",
                SubTotal = 100,
                ImpuestoTotal = 0,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
        [Fact]
        private async Task CrearVenta_sin_idUsuario_sin_idTipoDocumentoVenta_sin_nombreCliente_sin_documentoCliente()
        {
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Ventum>(context);
            var entity = new Ventum()
            {
                Total = 100,
                NumeroVenta = "00021",
                FechaRegistro = DateTime.Now,
                SubTotal = 100,
                ImpuestoTotal = 0,
            };
            var result = await repo.Create(entity);
            Assert.Equal(entity, result);
        }
    }
}