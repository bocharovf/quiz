using Microsoft.AspNetCore.Mvc;
using Moq;
using QuizService.Controllers;
using QuizService.Interfaces.Managers;
using QuizService.Model;
using QuizService.Model.DataContract;
using Xunit;

namespace QuizService.UnitTests
{
    public class QuizControllerTests
    {
        [Fact]
        public void Start_WhenQuizCreated_ReturnsOkResultWithQuiz()
        {
            // Arrange
            var quizStub = new Quiz()
            {
                Id = 1
            };
            var mockManager = new Mock<IQuizFlowManager>();
            mockManager.Setup(manager => manager.StartNewQuiz(It.IsAny<int>()))
                .Returns(quizStub);
            var controller = new QuizController(mockManager.Object);

            // Act
            var response = controller.Start(1);

            // Assert
            mockManager.Verify(mock => mock.StartNewQuiz(1), Times.Once);
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var quiz = Assert.IsType<Quiz>(objectResult.Value);
            Assert.Equal(1, quiz.Id);
        }

        [Fact]
        public void GetNextQuestion_WhenCommandReturned_ReturnsOkResultWithCommand()
        {
            // Arrange
            var commandStub = new QuizFlowCommandFinish();
            var mockManager = new Mock<IQuizFlowManager>();
            mockManager.Setup(manager => manager.GetNextQuestion(It.IsAny<int>()))
                .Returns(commandStub);
            var controller = new QuizController(mockManager.Object);

            // Act
            var response = controller.GetNextQuestion(1);

            // Assert
            mockManager.Verify(mock => mock.GetNextQuestion(1), Times.Once);
            var objectResult = Assert.IsType<OkObjectResult>(response);
            var quiz = Assert.IsType<QuizFlowCommandFinish>(objectResult.Value);
        }
    }
}
