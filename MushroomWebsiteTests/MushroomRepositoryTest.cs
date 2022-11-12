using System.Linq;
using MushroomWebsite.Repository;
using MushroomWebsite.Data;
using MushroomWebsite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

using MushroomWebsite.Repository;

namespace MushroomWebsiteTests
{
    public class MushroomRepositoryTest
    {
        private DbContextOptions<ApplicationDbContext> dbContextOptions;

        #region Constructor
        public MushroomRepositoryTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MushroomRepositoryTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = new ApplicationDbContext(dbContextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(
                new Mushroom { Name = "Mushroom1", Description = "aaa" },
                new Mushroom { Name = "Mushroom2", Description = "bbb" });

            context.SaveChanges();
        }
        #endregion

        #region GetMushroom
        [Fact]
        public void GetMushroom()
        {
            using var context = CreateContext();
            var repository = new MushroomRepository(context);

            var mushroom = repository.GetFirstOrDefault(c=>c.Id == 1);

            Assert.Equal("Mushroom1", mushroom.Name);
        }
        #endregion

        [Fact]
        public void GetAllMushrooms()
        {
            using var context = CreateContext();
            var repository = new MushroomRepository(context);

            var mushrooms = repository.GetAll();

            Assert.Collection(
                mushrooms,
                b => Assert.Equal("Mushroom1", b.Name),
                b => Assert.Equal("Mushroom2", b.Name));
        }

        [Fact]
        public void AddMushroom()
        {
            using var context = CreateContext();
            var repository = new MushroomRepository(context);

            Mushroom newMushroom = new Mushroom();
            newMushroom.Name = "Mushroom3";
            newMushroom.Description = "ccc";

            repository.Add(newMushroom);
            context.SaveChanges();

            var mushroom = repository.GetFirstOrDefault(c => c.Name.Equals("Mushroom3"));

            Assert.Equal("ccc", mushroom.Description);
        }

        ApplicationDbContext CreateContext() => new ApplicationDbContext(dbContextOptions, (context, modelBuilder) =>
        {
            #region ToInMemoryQuery
            modelBuilder.Entity<Mushroom>()
                .ToInMemoryQuery(() => context.Mushrooms.Select(b => new Mushroom { Name = b.Name }));
            #endregion
        });
    }
}
