using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneG14.Models;
using CapstoneG14.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CapstoneG14.Tests.RepositoryTest
{
    public class GenericRepostory_consultar
    {
        private BaeContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BaeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseConsultar")
                .Options;
            return new BaeContext(options);
        }

        [Fact]
        public async Task Consultar_RetrievesEntitiesBasedOnFilter()
        {
            // Preparar
            using var context = GetInMemoryDbContext();
            var repo = new GenericRepository<Usuario>(context);

            var entity1 = new Usuario { Correo = "Entity1@gmail", Clave = "123456" };
            var entity2 = new Usuario { Correo = "Entity2@gmail", Clave = "123456" };
            await repo.Create(entity1);
            await repo.Create(entity2);

            // Actuar
            var result = await repo.Consultar(e => e.Correo == "Entity1@gmail");
            // Verificar
            Assert.Single(result);
            Assert.Equal("Entity1@gmail", result.First().Correo);
        }




    }
}