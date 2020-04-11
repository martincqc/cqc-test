using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GFOL.Data;
using GFOL.Models;
using GFOL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace GFOL.Tests.Repository
{
    public class SubmissionRepositoryTests
    {
        [Fact]
        public async void TestFindByAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SubmissionDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repo = new SubmissionRepository(dbContext);
            // Act
            var result = await repo.FindByAsync(x => x.Id > 0);
            // Assert
            Assert.True(result is IEnumerable<Submission>);
        }
        [Fact]
        public async void TestCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SubmissionDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repo = new SubmissionRepository(dbContext);
            // Act
            var result = await repo.CreateAsync(new Submission {SavedDate = DateTime.Now, SubmissionJson = "{}"});
            // Assert
            Assert.True(result is Submission);
        }
        [Fact]
        public async void TestUpdateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SubmissionDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            var repo = new SubmissionRepository(dbContext);
            var newJson = "{id: 234}";
            // Act
            var newSubmission = await repo.CreateAsync(new Submission
                {SavedDate = DateTime.Now, SubmissionJson = "{id: 123}"});
            var id = newSubmission.Id;
            newSubmission.SubmissionJson = newJson;
            var result = await repo.UpdateAsync(id, newSubmission) as Submission;

            // Assert
            Assert.True(result.SubmissionJson == newJson);
        }
        [Fact]
        public async void TestDeleteAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SubmissionDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            var repo = new SubmissionRepository(dbContext);
            // Act
            var newSubmission = await repo.CreateAsync(new Submission
                {SavedDate = DateTime.Now, SubmissionJson = "{id: 123}"});
            var createdId = newSubmission.Id;
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
                .UseInMemoryDatabase(databaseName: "SubmissionDatabase")
                .Options;

            var dbContext = new ApplicationDbContext(options);
            var repo = new SubmissionRepository(dbContext);
            var json = "{id: 123}";
            // Act
            var newSubmission =
                await repo.CreateAsync(new Submission {SavedDate = DateTime.Now, SubmissionJson = json});
            var createdId = newSubmission.Id;

            var searchResult = await repo.GetByIdAsync(createdId);
            // Assert
            Assert.True(createdId > 0 && searchResult.SubmissionJson == json);
        }
    }
}
