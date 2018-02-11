using Microsoft.AspNetCore.Mvc;
using Moq;
using QuizService.BusinessLogic.Exceptions;
using QuizService.Controllers;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Managers;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using QuizService.Model.DataContract;
using Xunit;

namespace QuizService.UnitTests
{
    public class QuizControllerTests
    {
        #region Start

        [Fact]
        public void Start_WhenQuizCreated_ReturnsOkResultWithQuiz()
        {
            // Arrange
            var quizTemplateStub = new QuizTemplate()
            {
                Id = 1
            };

            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                .Returns(quizTemplateStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizTemplateRepository)
                   .Returns(mockQuizTemplateRepo.Object);

            var quizStub = new Quiz()
            {
                Id = 1
            };
            var mockManager = new Mock<IQuizFlowManager>();
            mockManager.Setup(manager => manager.StartNewQuiz(It.IsAny<QuizTemplate>()))
                       .Returns(quizStub);

            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act
            var response = controller.Start(1);

            // Assert
            mockManager.Verify(mock => mock.StartNewQuiz(quizTemplateStub), Times.Once);
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var quiz = Assert.IsType<Quiz>(objectResult.Value);
            Assert.Equal(1, quiz.Id);
        }

        [Fact]
        public void Start_WhenQuizTemplateNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizTemplateRepository)
                   .Returns(mockQuizTemplateRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();

            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.Start(1));
            Assert.Equal("QuizTemplate", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        #endregion

        #region GetNextQuestion

        [Fact]
        public void GetNextQuestion_WhenCommandReturned_ReturnsOkResultWithCommand()
        {
            // Arrange
            var commandStub = new QuizFlowCommandFinishContract();
            var quiz = new Quiz();

            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(value: quiz);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();
            mockManager.Setup(manager => manager.GetNextQuestion(It.IsAny<Quiz>()))
                       .Returns(commandStub);
            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act
            var response = controller.GetNextQuestion(1);

            // Assert
            mockManager.Verify(mock => mock.GetNextQuestion(quiz), Times.Once);
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var command = Assert.IsType<QuizFlowCommandFinishContract>(objectResult.Value);
        }

        [Fact]
        public void GetNextQuestion_WhenQuizNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();

            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.GetNextQuestion(1));
            Assert.Equal("Quiz", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        #endregion

        #region AnswerQuestion

        [Fact]
        public void AnswerQuestion_WhenValidArguments_ReturnsOkResult()
        {
            // Arrange
            var question = new Question()
            {
                Id = 2
            };
            var quiz = new Quiz();
            quiz.Questions.Add(question);

            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(value: quiz);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();
            mockManager.Setup(manager => manager.AnswerQuestion(It.IsAny<Question>(), It.IsAny<int>()));
            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act
            var response = controller.AnswerQuestion(1, 2, 3);

            // Assert
            mockManager.Verify(mock => mock.AnswerQuestion(question, 3), Times.Once);
            var objectResult = Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void AnswerQuestion_WhenQuizNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();
            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.AnswerQuestion(1, 2, 3));
            Assert.Equal("Quiz", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        [Fact]
        public void AnswerQuestion_WhenQuestionNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var quiz = new Quiz();
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(quiz);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();
            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.AnswerQuestion(1, 2, 3));
            Assert.Equal("Question", ex.EntityType);
            Assert.Equal(2, ex.EntityId);
        }

        #endregion

        #region CompleteQuiz

        [Fact]
        public void CompleteQuiz_WhenValidArguments_ReturnsOkResult()
        {
            // Arrange
            var quiz = new Quiz();
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(value: quiz);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();
            mockManager.Setup(manager => manager.CompleteQuiz(It.IsAny<Quiz>()));
            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act
            var response = controller.CompleteQuiz(1);

            // Assert
            mockManager.Verify(mock => mock.CompleteQuiz(quiz), Times.Once);
            var objectResult = Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void CompleteQuiz_WhenQuizNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                        .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.QuizRepository)
                   .Returns(mockQuizRepo.Object);

            var mockManager = new Mock<IQuizFlowManager>();
            var controller = new QuizFlowController(mockManager.Object, mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => controller.CompleteQuiz(1));
            Assert.Equal("Quiz", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        #endregion
    }
}
