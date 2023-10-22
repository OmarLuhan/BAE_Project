using CapstoneG14.Models;
using CapstoneG14.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CapstoneG14.Tests.RepositoryTest
{
    public class GenericRepository_Update
    {
        private BaeContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BaeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseUpdate")
                .Options;
            return new BaeContext(options);
        }

        [Fact]
        public async Task Update_UpdatesEntitySuccessfully()
        {
            // Preparar
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Usuario>(context);

            var entity = new Usuario { IdUsuario = 3, Correo = "OriginalName" };
            await repo.Create(entity);

            entity.Correo = "UpdatedCorreo";

            // Actuar
            var updateSuccess = await repo.Update(entity);

            // Verificar
            Assert.True(updateSuccess); // Verifica que el método Update devuelve true.

            var updatedEntity = await context.Set<Usuario>().FirstOrDefaultAsync(e => e.IdUsuario == entity.IdUsuario);
            Assert.Equal("UpdatedCorreo", updatedEntity.Correo); // Verifica que la entidad en la base de datos se haya actualizado correctamente.
        }

    }
}