using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using GFOL.Helpers;
using GFOL.Models;
using GFOL.Repository;
using GFOL.Services;
using Moq;
using Xunit;

namespace GFOL.Tests.Services
{
    public  class SubmissionServiceTests
    {
        [Fact]
        public async void TestGetFormById()
        {
            var testData = GetTestData();
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            mockRepo.Setup(x => x.FindByAsync(x => x.SavedDate < DateTime.Now)).ReturnsAsync(testData);
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            var result = await sut.GetAllSubmissionsAsync();
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void TestGetSubmissionAsync(int submissionId)
        {
            var testData = GetTestData();
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            mockRepo.Setup(x => x.GetByIdAsync(submissionId)).ReturnsAsync(testData.FirstOrDefault(x => x.Id == submissionId));
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            var result = await sut.GetSubmissionAsync(submissionId);
            result.Should().NotBeNull();
            result.Id.Should().Be(submissionId.ToString());
        }

        [Fact]
        public void TestGetSubmissionVm()
        {
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            //act
            var result = sut.GetSubmissionVm(true);
            //assert
            result.Should().NotBeNull();
            result.Answers.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void TestSaveSubmissionIntoSession()
        {
            var userSessionVm = new UserSessionVM();
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            mockSession.Setup(x => x.GetUserSessionVars()).Returns(userSessionVm).Verifiable();
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            //act
            var submissionVm = GetTestSubmissionVm();
            sut.SaveSubmissionIntoSession(submissionVm);
            //assert
            mockSession.Verify();
        }

        [Fact]
        public async void TestPostSubmissionAsync()
        {
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            var testSubmission = GetTestData()[0];
            mockRepo.Setup(x => x.CreateAsync(It.IsAny<Submission>())).ReturnsAsync(testSubmission).Verifiable();
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            //act
            var submissionVm = GetTestSubmissionVm();
            await sut.PostSubmissionAsync(submissionVm);
            //assert
            mockRepo.Verify();
        }
        [Fact]
        public void TestGetSubmissionFromSession()
        {
            var userSessionVm = new UserSessionVM{HasUserComeFromCheck = true, SubmissionVm = GetTestSubmissionVm()};
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            mockSession.Setup(x => x.GetUserSessionVars()).Returns(userSessionVm).Verifiable();
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            //act
            var result = sut.GetSubmissionFromSession();
            //assert
            result.Should().NotBeNull();
            mockRepo.Verify();
        }

        [Fact]
        public void TestSaveUserAnswerIntoSession()
        {
            var userSessionVm = new UserSessionVM { HasUserComeFromCheck = true, SubmissionVm = GetTestSubmissionVm() };
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            mockSession.Setup(x => x.GetUserSessionVars()).Returns(userSessionVm).Verifiable();
            mockSession.Setup(x => x.SetUserSessionVars(It.IsAny<UserSessionVM>())).Verifiable();
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            //act
            sut.SaveUserAnswerIntoSession(new Question{Answer = "yes", DataType = "string", InputType = "text", Options = "", QuestionId = "001", QuestionText = "blah", Validation = null});
            //assert
            mockSession.VerifyAll();
        }
        [Fact]
        public void TestGetUserAnswerFromSession()
        {
            var userSessionVm = new UserSessionVM { HasUserComeFromCheck = true, SubmissionVm = GetTestSubmissionVm() };
            var mockRepo = new Mock<IGenericRepository<Submission>>();
            var mockSchemaRepo = new Mock<IGenericRepository<Schema>>();
            var mockSession = new Mock<ISessionService>();
            mockSession.Setup(x => x.GetUserSessionVars()).Returns(userSessionVm).Verifiable();
            var sut = new SubmissionService(mockRepo.Object, mockSchemaRepo.Object, mockSession.Object);
            //act
            var expected = userSessionVm.SubmissionVm.Answers.Where(v => v.QuestionId == "001").FirstOrDefault()
                .AnswerText;
            var result = sut.GetUserAnswerFromSession("001");
            //assert
            result.Should().BeEquivalentTo(expected);
            mockSession.Verify();
        }
        private List<Submission> GetTestData()
        {
            var testData = new List<Submission>
            {
                new Submission {Id = 1, SavedDate = DateTime.Now, SubmissionJson = "{}"},
                new Submission {Id = 2, SavedDate = DateTime.Now, SubmissionJson = "{}"},
                new Submission {Id = 3, SavedDate = DateTime.Now, SubmissionJson = "{}"}
            };
            return testData;
        }

        private SubmissionVM GetTestSubmissionVm()
        {
            return new SubmissionVM
            {
                DateCreated = DateTime.Now.ToShortDateString(), Id = "123", FormName = "asd",
                Answers = new List<Answer>
                {
                    new Answer {AnswerText = "qwe", PageId = "1", QuestionId = "001", Question = "blah"},
                    new Answer {AnswerText = "qwe", PageId = "2", QuestionId = "002", Question = "blah"},
                    new Answer {AnswerText = "qwe", PageId = "3", QuestionId = "003", Question = "blah"}
                }
            };
        }
    }
}
