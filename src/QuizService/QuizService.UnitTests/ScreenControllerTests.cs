using Microsoft.AspNetCore.Mvc;
using Moq;
using QuizService.BusinessLogic.Exceptions;
using QuizService.Controllers;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Managers;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using QuizService.Model.DataContract;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuizService.UnitTests
{
    public class ScreenControllerTests
    {

        [Fact]
        public void GetQuizTemplates_WhenMultipleTemplates_ReturnsOkResultWithTemplatesArray()
        {
            // Arrange
            var quizTemplatesStub = new[] {
                new QuizTemplate(),
                new QuizTemplate()
            };

            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.Get(null, null))
                                .Returns(value: quizTemplatesStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizTemplateRepository)
                   .Returns(mockQuizTemplateRepo.Object);
            
            var controller = new ScreenController(mockUow.Object);

            // Act
            var response = controller.GetQuizTemplates();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var quizTemplates = Assert.IsAssignableFrom<IEnumerable<QuizTemplate>>(objectResult.Value);
            Assert.NotNull(quizTemplates);
            Assert.Equal(2, quizTemplates.Count());
        }

        [Fact]
        public void GetQuizTemplate_WhenTemplateFound_ReturnsOkResultWithTemplatesDetails()
        {
            // Arrange
            var quizTemplateStub = new QuizTemplate()
            {
                Id = 1
            };

            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                .Returns(value: quizTemplateStub);
            mockQuizTemplateRepo.Setup(repo => repo.GetQuestionTemplateCount(It.IsAny<int>()))
                                .Returns(value: 42);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizTemplateRepository)
                   .Returns(mockQuizTemplateRepo.Object);

            var controller = new ScreenController(mockUow.Object);

            // Act
            var response = controller.GetQuizTemplate(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var quizTemplateDetail = Assert.IsType<QuizTemplateDetailsContract>(objectResult.Value);
            Assert.Equal(42, quizTemplateDetail.QuestionsCount);
            Assert.NotNull(quizTemplateDetail.QuizTemplate);
            Assert.Equal(1, quizTemplateDetail.QuizTemplate.Id);
        }

        [Fact]
        public void GetQuizTemplate_WhenTemplateNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizTemplateRepository)
                   .Returns(mockQuizTemplateRepo.Object);

            var controller = new ScreenController(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.GetQuizTemplate(1));
            Assert.Equal("QuizTemplate", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        [Fact]
        public void GetScores_WhenMultipleScores_ReturnsOkResultWithScoresArray()
        {
            // Arrange
            var scoresStub = new[] {
                new Score(),
                new Score()
            };

            var mockScoreRepo = new Mock<IScoreRepository>();
            mockScoreRepo.Setup(repo => repo.Get(null, null))
                         .Returns(value: scoresStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.ScoreRepository)
                   .Returns(mockScoreRepo.Object);

            var controller = new ScreenController(mockUow.Object);

            // Act
            var response = controller.GetScores();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var scores = Assert.IsAssignableFrom<IEnumerable<Score>>(objectResult.Value);
            Assert.Equal(2, scores.Count());
        }

        [Fact]
        public void GetScore_WhenScoreFound_ReturnsOkResultWithScore()
        {
            // Arrange
            var scoreStub = new Score()
            {
                Id = 1
            };

            var mockScoreRepo = new Mock<IScoreRepository>();
            mockScoreRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                         .Returns(value: scoreStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.ScoreRepository)
                   .Returns(mockScoreRepo.Object);

            var controller = new ScreenController(mockUow.Object);

            // Act
            var response = controller.GetScore(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var score = Assert.IsType<Score>(objectResult.Value);
            Assert.Equal(1, score.Id);
        }

        [Fact]
        public void GetScore_WhenScoreNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockScoreRepo = new Mock<IScoreRepository>();
            mockScoreRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.ScoreRepository)
                   .Returns(mockScoreRepo.Object);

            var controller = new ScreenController(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.GetScore(1));
            Assert.Equal("Score", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }
    }
}
