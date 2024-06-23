using BAE_WEB.Models;
using BAE_WEB.Services.Implementations;
using BAE_WEB.Services.Interfaces;
using Moq;
using static BAE_WEB.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Tests.Services
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
        public async Task CrearLibro_DatosCorrectos_LibroRegistradoCorrectamente()
        {
            // Arrange
            var libroCrear = new Libro { CodigoBarra = "123456", Isbn = "10101010", IdEditorial = 1, IdGenero = 1 };
            var libroEsperado = new Libro
            {
                IdLibro = 1,
                CodigoBarra = libroCrear.CodigoBarra,
                Isbn = libroCrear.Isbn,
                IdEditorial = libroCrear.IdEditorial,
                IdGenero = libroCrear.IdGenero,
                IdEditorialNavigation = new Editorial { IdEditorial = 1, Descripcion = "Editorial 1" },
                IdGeneroNavigation = new Genero { IdGenero = 1, Descripcion = "Genero 1" }
            };

            _mockRepo.Setup(r => r.Obtener(l => l.CodigoBarra == libroCrear.CodigoBarra || l.Isbn == libroCrear.Isbn)).ReturnsAsync((Libro)null);
            _mockRepo.Setup(r => r.Create(libroCrear)).ReturnsAsync(libroEsperado);
            _mockRepo.Setup(r => r.Consultar(l => l.IdLibro == libroEsperado.IdLibro))
                                .ReturnsAsync(new List<Libro> { libroEsperado }.AsQueryable());

            // Act
            var libroCreado = await _libroService.Crear(libroCrear, null, "image/png");

            // Assert
            Assert.Equal(libroEsperado.IdLibro, libroCreado.IdLibro);
            Assert.NotNull(libroCreado.IdEditorialNavigation);
            Assert.NotNull(libroCreado.IdGeneroNavigation);

            // Verificar interacciones con los mocks
            _mockRepo.Verify(r => r.Obtener(l => l.CodigoBarra == libroCrear.CodigoBarra || l.Isbn == libroCrear.Isbn), Times.Once);
            _mockRepo.Verify(r => r.Create(libroCrear), Times.Once);
            _mockRepo.Verify(r => r.Consultar(l => l.IdLibro == libroEsperado.IdLibro), Times.Once);
        }
        [Fact]
        public async Task CrearLibro_DatosIncorrectos_Exeception()
        {

            // Arrange
            var libroCrear = new Libro { CodigoBarra = "123456", Isbn = "10101010", IdEditorial = 1, IdGenero = 1 };
            var libroExistente = new Libro
            {
                IdLibro = 1,
                CodigoBarra = libroCrear.CodigoBarra,
                Isbn = libroCrear.Isbn,
                IdEditorial = libroCrear.IdEditorial,
                IdGenero = libroCrear.IdGenero,
                IdEditorialNavigation = new Editorial { IdEditorial = 1, Descripcion = "Editorial 1" },
                IdGeneroNavigation = new Genero { IdGenero = 1, Descripcion = "Genero 1" }
            };

            _mockRepo.Setup(r => r.Obtener(l => l.CodigoBarra == libroCrear.CodigoBarra || l.Isbn == libroCrear.Isbn))
            .ReturnsAsync(libroExistente);
            try
            {
                await _libroService.Crear(libroCrear, null, "image/png");
            }
            catch (Exception e)
            {
                Assert.Equal("El codigo de barras o el isbn ya existe", e.Message);
            }
            // Verificar interacciones con los mocks
            _mockRepo.Verify(r => r.Obtener(l => l.CodigoBarra == libroCrear.CodigoBarra || l.Isbn == libroCrear.Isbn), Times.Once);

        }
        #endregion Crear
        #region Eliminar
        [Fact]
        public async Task EliminarLibro_DatosCorrectos_Ok()
        {
            // Arrange
            int idLibroEliminar = 1;
            var libroExistente = new Libro
            {
                IdLibro = idLibroEliminar,
                NombreImagen = "imagen.jpg"
            };
            _mockRepo.Setup(r => r.Obtener(l => l.IdLibro == idLibroEliminar)).ReturnsAsync(libroExistente);
            _mockRepo.Setup(r => r.Delete(libroExistente)).ReturnsAsync(true);
            // Act
            var resultado = await _libroService.Eliminar(idLibroEliminar);
            // Assert
            Assert.True(resultado);
            _mockRepo.Verify(r => r.Obtener(l => l.IdLibro == idLibroEliminar), Times.Once);
            _mockRepo.Verify(r => r.Delete(libroExistente), Times.Once);

        }
        [Fact]
        public async Task EliminarLibro_DatosIncorrectos_Exception()
        {
            // Arrange
            int idLibroEliminar = 1;
            _mockRepo.Setup(r => r.Obtener(l => l.IdLibro == idLibroEliminar)).ReturnsAsync((Libro)null);
            // Act
            bool resultado = false;
            try
            {
                resultado = await _libroService.Eliminar(idLibroEliminar);
            }
            catch (Exception ex)
            {
                Assert.Equal("No se encontro el libro", ex.Message);
            }
            // Assert
            Assert.True(!resultado);
            _mockRepo.Verify(r => r.Obtener(l => l.IdLibro == idLibroEliminar), Times.Once);

        }
        #endregion Eliminar
        #region Listar
        [Fact]
        public async Task ListarLibros_RetornaOK()
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
