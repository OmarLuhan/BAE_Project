using BAE_WEB.Models;
using BAE_WEB.Services.Implementations;
using BAE_WEB.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Frameworks;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace BAE_WEB.Tests.Services
{
    public class UsuarioServiceTest
    {
        private readonly Mock<IGenericRepository<Usuario>> _mockRepo;
        private readonly Mock<IFirebaseService> _mockFirebaseService;
        private readonly Mock<IUtilidadesService> _mockUtilidadesService;
        private readonly Mock<ICorreoService> _mockCorreoService;
        private readonly UsuarioService _usuarioService;
        public UsuarioServiceTest()
        {
            _mockRepo = new Mock<IGenericRepository<Usuario>>();
            _mockFirebaseService = new Mock<IFirebaseService>();
            _mockUtilidadesService = new Mock<IUtilidadesService>();
            _mockCorreoService = new Mock<ICorreoService>();
            _usuarioService = new UsuarioService(_mockRepo.Object, _mockFirebaseService.Object, _mockUtilidadesService.Object, _mockCorreoService.Object);
        }
        #region ObtenerPorID
        [Fact]
        public async Task ObtenerPorId_Retorna_1_usuario()
        {
            int idUsuarioBuscado = 1;
            var usuarioEsperado = new Usuario
            {
                IdUsuario = idUsuarioBuscado,
                Correo = "N00209455@upn.pe"
            };
            // Simulando la respuesta del repositorio al consultar un usuario por su ID.
            _mockRepo.Setup(r => r.Consultar(u => u.IdUsuario == idUsuarioBuscado))
                .ReturnsAsync(new List<Usuario> { usuarioEsperado }.AsQueryable());
            // Act
            var resultado = await _usuarioService.ObtenerPorId(idUsuarioBuscado);
            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(idUsuarioBuscado, resultado.IdUsuario);
            Assert.Equal("N00209455@upn.pe", resultado.Correo);

        }
        #endregion ObtenerPorID
        #region CrearUsuario
        [Fact]
        public async Task Crear_Usuario_DatosCoreectos()
        {
            var usuario_crear = new Usuario
            {
                Nombre = "Test",
                Correo = "test1@test.com",
                Telefono = "123456779",
                IdRol = 1,
                UrlFoto = "",
                NombreFoto = "",
                Clave = "",
                EsActivo = true,
                FechaRegistro = DateTime.Now
            };
            var usuario_esperado = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Test",
                Correo = "test1@test.com",
                Telefono = "123456779",
                IdRol = 1,
                UrlFoto = "",
                NombreFoto = "",
                Clave = "",
                EsActivo = true,
                FechaRegistro = DateTime.Now,
                IdRolNavigation = new Rol { IdRol = 1, Descripcion = "Administrador" }
            };
            _mockRepo.Setup(r => r.Obtener(u => u.Correo == usuario_crear.Correo))
               .ReturnsAsync((Usuario)null); // Simula que no se encontró un usuario existente
            _mockUtilidadesService.Setup(u => u.GenerarClave()).Returns("ClaveGenerada");
            _mockUtilidadesService.Setup(u => u.ConvertirSha256("ClaveGenerada")).Returns("ClaveEncriptada");
            usuario_crear.Clave = "ClaveEncriptada";
            _mockRepo.Setup(r => r.Create(usuario_crear)).ReturnsAsync(usuario_esperado);
            _mockRepo.Setup(r => r.Consultar(u => u.IdUsuario == usuario_esperado.IdUsuario))
                                    .ReturnsAsync(new List<Usuario> { usuario_esperado }.AsQueryable());


            var result = await _usuarioService.Crear(usuario_crear, Stream.Null, "", "");
            Assert.Equal(usuario_esperado.IdUsuario, result.IdUsuario);
            Assert.NotNull(result.IdRolNavigation);
            _mockRepo.Verify(r => r.Create(It.Is<Usuario>(u => u.Correo == usuario_crear.Correo && u.Clave == usuario_crear.Clave)), Times.Once);
            _mockRepo.Verify(r => r.Consultar(u => u.IdUsuario == usuario_esperado.IdUsuario), Times.Once);
        }
        [Fact]
        public async Task CrearUsuario_DatosIncorrectos()
        {

            var usuario = new Usuario
            {
                Correo = "test1@test.com"
            };
            var usuarioEncontrado = new Usuario
            {
                Correo = "test1@test.com"
            };
            _mockRepo.Setup(r => r.Obtener(u => u.Correo == usuario.Correo))
           .ReturnsAsync(usuarioEncontrado); // Simula que si se encontró un usuario existente
            try
            {
                var resultado = await _usuarioService.Crear(usuario, null, "", "");
            }
            catch (Exception e)
            {
                Assert.Equal("El correo ya existe", e.Message);
            }

        }
        [Fact]
        public async Task CrearUsuario_DatosIncorrectos_Ecxeption()
        {
            var usuario = new Usuario
            {
                Nombre = "Test",
                Correo = "test1@test.com",
                Telefono = "123456779",
                IdRol = 1,
                UrlFoto = "",
                NombreFoto = "",
                Clave = "",
                EsActivo = true,
                FechaRegistro = DateTime.Now
            };
            _mockRepo.Setup(r => r.Obtener(u => u.Correo == usuario.Correo))
               .ReturnsAsync((Usuario)null); // Simula que no se encontró un usuario existente
            _mockUtilidadesService.Setup(u => u.GenerarClave()).Returns("ClaveGenerada");
            _mockUtilidadesService.Setup(u => u.ConvertirSha256("ClaveGenerada")).Returns("ClaveEncriptada");
            usuario.Clave = "ClaveEncriptada";
            _mockRepo.Setup(r => r.Create(usuario)).ReturnsAsync(usuario);
            var result = new Usuario();
            try
            {
                result = await _usuarioService.Crear(usuario, null, "", "");
            }
            catch (Exception e)
            {
                Assert.Equal("No se pudo crear el usuario", e.Message);
            }
        }
        #endregion CrearUsuario
        #region EliminarUsuario
        [Fact]
        public async Task EliminarUsuario_OK()
        {
            // Arrange
            int idUsuario = 1;
            string nombreFoto = "foto.jpg";

            var usuarioFalso = new Usuario
            {
                IdUsuario = idUsuario,
                NombreFoto = nombreFoto
            };

            _mockRepo.Setup(r => r.Obtener(u => u.IdUsuario == idUsuario))
                     .ReturnsAsync(usuarioFalso);
            _mockRepo.Setup(r => r.Delete(usuarioFalso))
                     .ReturnsAsync(true);
            var resultado = await _usuarioService.Eliminar(idUsuario);

            Assert.True(resultado);
            _mockRepo.Verify(r => r.Delete(usuarioFalso), Times.Once);
            _mockFirebaseService.Verify(f => f.EliminarStorage("carpeta_usuario", nombreFoto), Times.Once);

        }
        [Fact]
        public async Task Eliminar_100_UsuarioNoExistente_DebeLanzarExcepcion()
        {
            int idUsuario = 1;
            _mockRepo.Setup(r => r.Obtener(u => u.IdUsuario == idUsuario)).ReturnsAsync((Usuario)null);
            try
            {
                await _usuarioService.Eliminar(idUsuario);
            }
            catch (Exception e)
            {
                Assert.Equal("No se encontro el usuario", e.Message);
            }
        }
        #endregion EliminarUsuario
        #region GuardarPerfil
        [Fact]
        public async Task Guardar_100_Perfil_Correctamente()
        {
            for (int i = 1; i < 100; i++)
            {
                // Arrange
                var usuarioEnontrado = new Usuario { IdUsuario = i, Correo = "original@example.com" + i, Telefono = i + "23456789" };
                var usuarioActualizar = new Usuario { IdUsuario = i, Correo = "updated@example.com" + i, Telefono = i + "98765432" };

                _mockRepo.Setup(r => r.Obtener(u => u.IdUsuario == usuarioActualizar.IdUsuario))
                    .ReturnsAsync(usuarioEnontrado);
                _mockRepo.Setup(r => r.Update(usuarioEnontrado))
                    .ReturnsAsync(true); // Simula una actualización exitosa
                                         // Act
                var resultado = await _usuarioService.GuardarPerfil(usuarioActualizar);

                // Assert
                Assert.True(resultado);
                _mockRepo.Verify(r => r.Update(It.Is<Usuario>(u => u.Correo == "updated@example.com" + i && u.Telefono == i + "98765432")), Times.Once);
            }
        }
        [Fact]
        public async Task Guardar_100_Perfil_Correctamente01()
        {
            for (int i = 1; i < 100; i++)
            {
                // Arrange
                var usuarioEnontrado = new Usuario { IdUsuario = i, Correo = "original@example.com" + i, Telefono = i + "1234567890" };
                var usuarioActualizar = new Usuario { IdUsuario = i, Correo = "updated@example.com" + i, Telefono = i + "0987654321" };

                _mockRepo.Setup(r => r.Obtener(u => u.IdUsuario == usuarioActualizar.IdUsuario))
                    .ReturnsAsync(usuarioEnontrado);
                _mockRepo.Setup(r => r.Update(usuarioEnontrado))
                    .ReturnsAsync(true); // Simula una actualización exitosa
                                         // Act
                var resultado = await _usuarioService.GuardarPerfil(usuarioActualizar);

                // Assert
                Assert.True(resultado);
                _mockRepo.Verify(r => r.Update(It.Is<Usuario>(u => u.Correo == "updated@example.com" + i && u.Telefono == i + "0987654321")), Times.Once);
            }
        }
        [Fact]
        public async Task Guardar_100_Perfil_NoExiste_LanzaExcepcion()
        {
            for (int i = 1; i < 100; i++)
            {
                // Arrange
                var usuarioActualizar = new Usuario { IdUsuario = i, Correo = "nonexistent@example.com" + i, Telefono = "00000000" + i };

                _mockRepo.Setup(r => r.Obtener(u => u.IdUsuario == usuarioActualizar.IdUsuario))
                    .ReturnsAsync((Usuario)null); // Simula que no se encontró el usuario
                try
                {
                    var resultado = await _usuarioService.GuardarPerfil(usuarioActualizar);
                }
                catch (Exception e)
                {
                    Assert.Equal("No se encontro el usuario", e.Message);
                }
            }

        }
        [Fact]
        public async Task Guardar_100_Perfil_NoExiste_LanzaExcepcion01()
        {
            for (int i = 1; i < 100; i++)
            {
                // Arrange
                var usuarioActualizar = new Usuario { IdUsuario = i, Correo = "nonexistent@example.com" + i, Telefono = "00000000" + i };

                _mockRepo.Setup(r => r.Obtener(u => u.IdUsuario == usuarioActualizar.IdUsuario))
                    .ReturnsAsync((Usuario)null); // Simula que no se encontró el usuario
                try
                {
                    var resultado = await _usuarioService.GuardarPerfil(usuarioActualizar);
                }
                catch (Exception e)
                {
                    Assert.Equal("No se encontro el usuario", e.Message);
                }
            }

        }

        #endregion GuardarPerfil
        #region ListarUsuarios
        [Fact]
        public async Task Lista_DevuelveListaUsuariosConRoles()
        {
            // Arrange
            var usuariosMock = new List<Usuario>
        {
            new(){ IdUsuario = 1, Nombre = "Usuario1", IdRolNavigation = new Rol {IdRol=1,Descripcion="Administrador"} },
            new(){ IdUsuario = 2, Nombre = "Usuario2", IdRolNavigation = new Rol {IdRol=1,Descripcion="Administrador" } },
            new(){ IdUsuario = 3, Nombre = "Usuario3", IdRolNavigation = new Rol {IdRol=2,Descripcion="Cliente" } },
            new(){ IdUsuario = 4, Nombre = "Usuario4", IdRolNavigation = new Rol {IdRol=2,Descripcion="Cliente" } },
            new(){ IdUsuario = 5, Nombre = "Usuario5", IdRolNavigation = new Rol {IdRol=3,Descripcion="Empleado" } },
            // Agrega más usuarios según sea necesario
        }.AsQueryable();
            var mockSet = new Mock<DbSet<Usuario>>();
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(usuariosMock.Provider);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(usuariosMock.Expression);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(usuariosMock.ElementType);
            mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(usuariosMock.GetEnumerator());

            _mockRepo.Setup(r => r.Consultar(null)).ReturnsAsync(mockSet.Object);

            // Act
            var resultado = await _usuarioService.Lista();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuariosMock.Count(), resultado.Count);
            foreach (var usuario in resultado)
            {
                Assert.NotNull(usuario.IdRolNavigation); // Asegúrate de que se incluya la información del rol
            }
        }
        #endregion ListarUsuarios
    }
}