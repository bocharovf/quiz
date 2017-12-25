using Moq;
using QuizService.BusinessLogic.Exceptions;
using QuizService.BusinessLogic.QuizFlow;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using QuizService.Model.DataContract;
using System;
using Xunit;

namespace QuizService.BusinessLogic.UnitTests
{
    public class QuizFlowManagerTests
    {
        #region StartNewQuiz

        [Fact]
        public void StartNewQuiz_WhenTemplateFound_ReturnsNewQuiz()
        {
            // Arrange
            var quizTemplateStub = new QuizTemplate() {
                Id = 1
            };
            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(quizTemplateStub);

            var mockQuizRepo = new Mock<IQuizRepository>();

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizTemplateRepository)
                .Returns(mockQuizTemplateRepo.Object);
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object);

            // Act
            var quiz = manager.StartNewQuiz(1);

            // Assert
            Assert.Equal(1, quiz.Template.Id);
            mockQuizRepo.Verify(repo => repo.Insert(It.IsAny<Quiz>()), Times.Once);
            mockUow.Verify(uow => uow.Save(), Times.Once);
        }

        [Fact]
        public void StartNewQuiz_WhenTemplateNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizTemplateRepository)
                .Returns(mockQuizTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => manager.StartNewQuiz(1));
            Assert.Equal("QuizTemplate", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        #endregion

        #region GetNextQuestion

        [Fact]
        public void GetNextQuestion_WhenQuizNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => manager.GetNextQuestion(1));
            Assert.Equal("Quiz", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        [Fact]
        public void GetNextQuestion_WhenLastQuestionNotAnswered_ThrowsQuizFlow()
        {
            // Arrange
            var quizStub = new Quiz();
            var questionStub = new Question() {
                Order = 1,
                DateStart = DateTime.Now,
                DateEnd = null
            };
            quizStub.Questions.Add(questionStub);

            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(quizStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<QuizFlowException>(() => manager.GetNextQuestion(1));
            Assert.Equal(QuizFlowErrorCodes.LastQuestionNotAnswered.ToString(), ex.ErrorCode);
        }

        [Fact]
        public void GetNextQuestion_WhenNoNextQuestionFound_ReturnsQuizFinishCommand()
        {
            // Arrange
            var quizStub = new Quiz();
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(quizStub);
            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetQuestionTemplate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);
            mockUow.Setup(repo => repo.QuizTemplateRepository)
                .Returns(mockQuizTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object);

            // Act
            var command = manager.GetNextQuestion(1);

            // Assert
            var commandFinish = Assert.IsType<QuizFlowCommandFinish>(command);
        }

        [Fact]
        public void GetNextQuestion_WhenNewQuizAndNextQuestionFound_ReturnsQuizProceedCommand()
        {
            // Arrange
            var quizStub = new Quiz();
            var questionTemplate = new QuestionTemplate() {
                Id = 1
            };
            var mockQuizRepo = new Mock<IQuizRepository>();
            mockQuizRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(quizStub);
            var mockQuizTemplateRepo = new Mock<IQuizTemplateRepository>();
            mockQuizTemplateRepo.Setup(repo => repo.GetQuestionTemplate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(questionTemplate);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);
            mockUow.Setup(repo => repo.QuizTemplateRepository)
                .Returns(mockQuizTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object);

            // Act
            var command = manager.GetNextQuestion(1);

            // Assert
            var commandProceed = Assert.IsType<QuizFlowCommandProceed>(command);
            Assert.NotNull(commandProceed.Question);
            Assert.Equal(1, commandProceed.Question.Template.Id);
            Assert.NotNull(commandProceed.Question.DateStart);
            Assert.Equal(1, commandProceed.Question.Order);
        }

        #endregion
    }
}
