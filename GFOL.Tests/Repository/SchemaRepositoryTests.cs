using System;
using System.Collections.Generic;
using System.Linq;
using GFOL.Data;
using GFOL.Models;
using GFOL.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GFOL.Tests.Repository
{
    public class RepositoryTests
    {
        [Fact]
        public async void TestFindByAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SchemaDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repo = new SchemaRepository(dbContext);
            // Act
            var result = await repo.FindByAsync(x => x.Id > 0);
            // Assert
            Assert.True(result is IEnumerable<Schema>);
        }
        [Fact]
        public async void TestCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SchemaDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repo = new SchemaRepository(dbContext);
            // Act
            var result = await repo.CreateAsync(new Schema {SavedDate = DateTime.Now, SchemaJson = "{}"});
            // Assert
            Assert.True(result is Schema);
        }
        [Fact]
        public async void TestUpdateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SchemaDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            var repo = new SchemaRepository(dbContext);
            var newJson = "{id: 234}";
            // Act
            var newSchema = await repo.CreateAsync(new Schema() {SavedDate = DateTime.Now, SchemaJson = "{id: 123}"});
            var id = newSchema.Id;
            newSchema.SchemaJson = newJson;
            var result = await repo.UpdateAsync(id, newSchema);

            // Assert
            Assert.True(result.SchemaJson == newJson);
        }
        [Fact]
        public async void TestDeleteAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SchemaDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            var repo = new SchemaRepository(dbContext);
            // Act
            var newSchema = await repo.CreateAsync(new Schema {SavedDate = DateTime.Now, SchemaJson = "{id: 123}"});
            var createdId = newSchema.Id;
            await repo.DeleteAsync(createdId);

            var searchResult = await repo.FindByAsync(x => x.Id == createdId);

            if (searchResult.Count() == 0)
            {

            }
            // Assert
            Assert.True(createdId > 0 && searchResult.Count() == 0);
        }
        [Fact]
        public async void TestGetByIdAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SchemaDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            var repo = new SchemaRepository(dbContext);
            var json = "{id: 123}";
            // Act
            var newSchema = await repo.CreateAsync(new Schema { SavedDate = DateTime.Now, SchemaJson = json });
            var createdId = newSchema.Id;

            var searchResult = await repo.GetByIdAsync(createdId);
            // Assert
            Assert.True(createdId > 0 && searchResult.SchemaJson == json);
        }
    }
}
