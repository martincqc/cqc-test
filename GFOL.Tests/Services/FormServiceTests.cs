using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using GFOL.Data;
using GFOL.Models;
using GFOL.Repository;
using GFOL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Xunit;

namespace GFOL.Tests.Services
{
    public class FormServiceTests
    {
        [Theory]
        [InlineData("001")]
        [InlineData("002")]
        [InlineData("003")]
        [InlineData("004")]
        [InlineData("005")]
        public async void TestGetFormById(string pageId)
        {
            var mockRepo = new Mock<IGenericRepository<Schema>>();
            var sut = new FormService(mockRepo.Object);
            var result = sut.GetPageById(pageId);
            result.Should().NotBe(null);
            result.Questions.Count.Should().BeGreaterThan(0);
        }
    }
}
