
using CapstoneG14.Models;
using CapstoneG14.Services.Implementations;
using CapstoneG14.Services.Interfaces;
using Moq;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Tests.Services
{
    public class LibroServiceTest
    {
        private readonly Mock<IGenericRepository<Libro>> _mockRepo;
        private readonly Mock<IFirebaseService> _mockFirebaseService;
        private readonly ILibroService _libroService;
        public LibroServiceTest()
        {
            _mockRepo = new Mock<IGenericRepository<Libro>>();
            _mockFirebaseService = new Mock<IFirebaseService>();
            _libroService = new LibroService(_mockRepo.Object, _mockFirebaseService.Object);
        }
        #region Crear
        [Fact]
        public async Task Crear_DebeCrearLibroConRelacionesCuandoNoExiste()
        {
            // Arrange
            var libroParaCrear = new Libro { CodigoBarra = "123456", Isbn = "10101010", IdEditorial = 1, IdGenero = 1 };
            var libroCreadoEsperado = new Libro
            {
                IdLibro = 1,
                CodigoBarra = libroParaCrear.CodigoBarra,
                Isbn = libroParaCrear.Isbn,
                IdEditorial = libroParaCrear.IdEditorial,
                IdGenero = libroParaCrear.IdGenero,
                IdEditorialNavigation = new Editorial { IdEditorial = 1, Descripcion = "Editorial 1" },
                IdGeneroNavigation = new Genero { IdGenero = 1, Descripcion = "Genero 1" }
            };

            _mockRepo.Setup(r => r.Obtener(l => l.CodigoBarra == libroParaCrear.CodigoBarra || l.Isbn == libroParaCrear.Isbn)).ReturnsAsync((Libro)null);
            _mockRepo.Setup(r => r.Create(libroParaCrear)).ReturnsAsync(libroCreadoEsperado);
            _mockRepo.Setup(r => r.Consultar(l => l.IdLibro == libroCreadoEsperado.IdLibro))
                                .ReturnsAsync(new List<Libro> { libroCreadoEsperado }.AsQueryable());

            // Act
            var libroCreado = await _libroService.Crear(libroParaCrear, null, "image/png");

            // Assert
            Assert.Equal(libroCreadoEsperado.IdLibro, libroCreado.IdLibro);
            Assert.NotNull(libroCreado.IdEditorialNavigation);
            Assert.NotNull(libroCreado.IdGeneroNavigation);

            // // Verificar interacciones con los mocks
            _mockRepo.Verify(r => r.Obtener(l => l.CodigoBarra == libroParaCrear.CodigoBarra || l.Isbn == libroParaCrear.Isbn), Times.Once);
            _mockRepo.Setup(r => r.Create(libroParaCrear)).ReturnsAsync(libroCreadoEsperado);
            _mockRepo.Verify(r => r.Consultar(l => l.IdLibro == libroCreadoEsperado.IdLibro), Times.Once);
        }
        [Fact]
        public async Task Crear_DebeCrear_100_LibroConRelacionesCuandoNoExiste()
        {
            for (int i = 1; i < 5; i++)
            {
                // Arrange
                var libroParaCrear = new Libro { CodigoBarra = "123456" + i, Isbn = "10101010" + i, IdEditorial = i, IdGenero = i };
                var libroCreadoEsperado = new Libro
                {
                    IdLibro = i,
                    CodigoBarra = libroParaCrear.CodigoBarra,
                    Isbn = libroParaCrear.Isbn,
                    IdEditorial = libroParaCrear.IdEditorial,
                    IdGenero = libroParaCrear.IdGenero,
                    IdEditorialNavigation = new Editorial { IdEditorial = i, Descripcion = "Editorial 1" },
                    IdGeneroNavigation = new Genero { IdGenero = i, Descripcion = "Genero 1" }
                };
                _mockRepo.Setup(r => r.Obtener(l => l.CodigoBarra == libroParaCrear.CodigoBarra || l.Isbn == libroParaCrear.Isbn)).ReturnsAsync((Libro)null);
                _mockRepo.Setup(r => r.Create(libroParaCrear)).ReturnsAsync(libroCreadoEsperado);
                _mockRepo.Setup(r => r.Consultar(l => l.IdLibro == libroCreadoEsperado.IdLibro))
                                    .ReturnsAsync(new List<Libro> { libroCreadoEsperado }.AsQueryable());
                // Act
                var libroCreado = await _libroService.Crear(libroParaCrear, null, "image/png");

                // Assert
                Assert.Equal(libroCreadoEsperado.IdLibro, libroCreado.IdLibro);
                Assert.NotNull(libroCreado.IdEditorialNavigation);
                Assert.NotNull(libroCreado.IdGeneroNavigation);

                // // Verificar interacciones con los mocks
                _mockRepo.Verify(r => r.Obtener(l => l.CodigoBarra == libroParaCrear.CodigoBarra || l.Isbn == libroParaCrear.Isbn), Times.Once);
                _mockRepo.Setup(r => r.Create(libroParaCrear)).ReturnsAsync(libroCreadoEsperado);
                _mockRepo.Verify(r => r.Consultar(l => l.IdLibro == libroCreadoEsperado.IdLibro), Times.Once);
            }

        }
        [Fact]
        public void Crear_100_Libros_existentes_exeption()
        {
            for (int i = 1; i < 5; i++)
            {
                // Arrange
                var libroParaCrear = new Libro { CodigoBarra = "123456" + i, Isbn = "10101010" + i, IdEditorial = i, IdGenero = i };
                var libroCreadoEsperado = new Libro
                {
                    IdLibro = i,
                    CodigoBarra = libroParaCrear.CodigoBarra,
                    Isbn = libroParaCrear.Isbn,
                    IdEditorial = libroParaCrear.IdEditorial,
                    IdGenero = libroParaCrear.IdGenero,
                    IdEditorialNavigation = new Editorial { IdEditorial = i, Descripcion = "Editorial 1" },
                    IdGeneroNavigation = new Genero { IdGenero = i, Descripcion = "Genero 1" }
                };
                _mockRepo.Setup(r => r.Obtener(l => l.CodigoBarra == libroParaCrear.CodigoBarra || l.Isbn == libroParaCrear.Isbn))
                .ReturnsAsync(libroCreadoEsperado);
                try
                {
                    _libroService.Crear(libroParaCrear, null, "image/png");

                }
                catch (Exception e)
                {
                    Assert.Equal("El codigo de barras o el isbn ya existe", e.Message);
                }
                // // Verificar interacciones con los mocks
                _mockRepo.Verify(r => r.Obtener(l => l.CodigoBarra == libroParaCrear.CodigoBarra || l.Isbn == libroParaCrear.Isbn), Times.Once);
            }
        }
        #endregion Crear
        #region Eliminar
        [Fact]
        public async Task Eliminar_100_EliminarLibroExiste()
        {
            for (int i = 1; i < 5; i++)
            {
                // Arrange
                int idLibroParaEliminar = i;
                var libroExistente = new Libro
                {
                    IdLibro = idLibroParaEliminar,
                    NombreImagen = "imagen.jpg"
                };

                _mockRepo.Setup(r => r.Obtener(l => l.IdLibro == idLibroParaEliminar)).ReturnsAsync(libroExistente);
                _mockRepo.Setup(r => r.Delete(libroExistente)).ReturnsAsync(true);
                // Act
                var resultado = await _libroService.Eliminar(idLibroParaEliminar);

                // Assert
                Assert.True(resultado);
                _mockRepo.Verify(r => r.Obtener(l => l.IdLibro == idLibroParaEliminar), Times.Once);
                _mockRepo.Verify(r => r.Delete(libroExistente), Times.Once);
            }
        }
        [Fact]
        public async Task Eliminar_100_libros_No_existentes()
        {
            for (int i = 1; i < 4; i++)
            {
                int idLibroParaEliminar = i;
                _mockRepo.Setup(r => r.Obtener(l => l.IdLibro == idLibroParaEliminar)).ReturnsAsync((Libro)null);
                try
                {
                    await _libroService.Eliminar(idLibroParaEliminar);
                }
                catch (Exception e)
                {
                    Assert.Equal("No se encontro el libro", e.Message);
                }
                _mockRepo.Verify(r => r.Obtener(l => l.IdLibro == idLibroParaEliminar), Times.Once);
            }
        }
        #endregion Eliminar
        #region Listar
        [Fact]
        public async Task Lista_DebeRetornarListaDeLibros()
        {
            // Arrange
            var librosMock = new List<Libro>
        {
            new () {
                CodigoBarra = "123456",
                Isbn = "10101010",
                IdEditorial = 1,
                IdGenero = 1,
                IdEditorialNavigation = new Editorial { IdEditorial = 1, Descripcion = "Editorial 1" },
                IdGeneroNavigation = new Genero { IdGenero = 1, Descripcion = "Genero 1" }
                },
            new ()
            {
                CodigoBarra = "654321",
                Isbn = "2020",
                IdEditorial = 1,
                IdGenero = 1,
                IdEditorialNavigation = new Editorial { IdEditorial = 1, Descripcion = "Editorial 1" },
                IdGeneroNavigation = new Genero { IdGenero = 1, Descripcion = "Genero 1" }
            }
        }.AsQueryable();

            var mockSet = new Mock<IQueryable<Libro>>();
            mockSet.As<IQueryable<Libro>>().Setup(m => m.Provider).Returns(librosMock.Provider);
            mockSet.As<IQueryable<Libro>>().Setup(m => m.Expression).Returns(librosMock.Expression);
            mockSet.As<IQueryable<Libro>>().Setup(m => m.ElementType).Returns(librosMock.ElementType);
            mockSet.As<IQueryable<Libro>>().Setup(m => m.GetEnumerator()).Returns(librosMock.GetEnumerator());

            _mockRepo.Setup(r => r.Consultar(null)).ReturnsAsync(mockSet.Object);
            // Act
            var resultado = await _libroService.Lista();
            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(librosMock.Count(), resultado.Count);
            _mockRepo.Verify(r => r.Consultar(null), Times.Once);
        }

        #endregion Listar
    }
}
