using CapstoneG14.Services.Implementations;
using CapstoneG14.Services.Interfaces;
using Moq;

namespace CapstoneG14.Tests.Services
{
    public class FirebaseServiceTest
    {
        [Fact]
        public async Task SubirStorage()
        {
            Stream? foto = null;
            var mock = new Mock<IFirebaseService>();
            mock.Setup(x => x.SubirStorage(It.IsAny<Stream>(), "destino", "foto.webp")).ReturnsAsync("");
            var firebaseService = mock.Object;
            var result = await firebaseService.SubirStorage(It.IsAny<Stream>(), "carpeta_usuario", "foto.webp");
            Assert.Null(result);
        }
    }
}