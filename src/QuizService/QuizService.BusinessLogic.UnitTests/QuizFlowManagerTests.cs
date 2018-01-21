using Moq;
using QuizService.BusinessLogic.Exceptions;
using QuizService.BusinessLogic.QuizFlow;
using QuizService.BusinessLogic.Scores;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using QuizService.Model.DataContract;
using QuizService.Model.Exceptions;
using System;
using System.Linq;
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
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object, null);
            var quizTemplate = new QuizTemplate()
            {
                Id = 1
            };

            // Act
            var quiz = manager.StartNewQuiz(quizTemplate);

            // Assert
            Assert.Equal(1, quiz.TemplateId);
            mockQuizRepo.Verify(repo => repo.Insert(It.IsAny<Quiz>()), Times.Once);
            mockUow.Verify(uow => uow.Save(), Times.Once);
        }

        #endregion

        #region GetNextQuestion

        [Fact]
        public void GetNextQuestion_WhenCurrentQuestionNotAnswered_ReturnsQuizProceedCommandWithCurrentQuestion()
        {
            // Arrange
            var quiz = new Quiz()
            {
                Id = 1
            };
            var currentQuestion = new Question()
            {
                Id = 2,
                Quiz = quiz
            };
            quiz.Questions.Add(currentQuestion);

            var questionTemplate = new QuestionTemplate();
            questionTemplate.Answers.Add(new AnswerTemplate()
            {
                IsCorrect = false
            });
            questionTemplate.Answers.Add(new AnswerTemplate()
            {
                IsCorrect = true
            });

            var mockQuizRepo = new Mock<IQuizRepository>();
            var mockQuestionTemplateRepo = new Mock<IQuestionTemplateRepository>();
            mockQuestionTemplateRepo
                .Setup(repo => repo.GetByID(It.IsAny<int>()))
                .Returns(questionTemplate);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);
            mockUow.Setup(repo => repo.QuestionTemplateRepository)
                .Returns(mockQuestionTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act
            var command = manager.GetNextQuestion(quiz);

            // Assert
            var commandProceed = Assert.IsType<QuizFlowCommandProceedContract>(command);
            Assert.Equal(1, commandProceed.QuizId);
            Assert.Equal(2, commandProceed.QuestionId);
            Assert.Equal(2, commandProceed.Template.Answers.Count);
            Assert.DoesNotContain(commandProceed.Template.Answers, answer => answer.IsCorrect);
        }

        [Fact]
        public void GetNextQuestion_WhenNoNextQuestionFound_ReturnsQuizFinishCommand()
        {
            // Arrange
            var quiz = new Quiz();
            var mockQuizRepo = new Mock<IQuizRepository>();
            var mockQuestionTemplateRepo = new Mock<IQuestionTemplateRepository>();
            mockQuestionTemplateRepo.Setup(repo => repo.GetQuestionTemplate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(value: null);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);
            mockUow.Setup(repo => repo.QuestionTemplateRepository)
                .Returns(mockQuestionTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act
            var command = manager.GetNextQuestion(quiz);

            // Assert
            var commandFinish = Assert.IsType<QuizFlowCommandFinishContract>(command);
        }

        [Fact]
        public void GetNextQuestion_WhenNextQuestionFound_ReturnsQuizProceedCommandWithNextQuestion()
        {
            // Arrange
            var quiz = new Quiz()
            {
                Id = 1
            };
            var questionTemplate = new QuestionTemplate();
            questionTemplate.Answers.Add(new AnswerTemplate()
            {
                IsCorrect = false
            });
            questionTemplate.Answers.Add(new AnswerTemplate()
            {
                IsCorrect = true
            });

            var mockQuizRepo = new Mock<IQuizRepository>();
            var mockQuestionTemplateRepo = new Mock<IQuestionTemplateRepository>();
            mockQuestionTemplateRepo
                .Setup(repo => repo.GetQuestionTemplate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(questionTemplate);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuizRepository)
                .Returns(mockQuizRepo.Object);
            mockUow.Setup(repo => repo.QuestionTemplateRepository)
                .Returns(mockQuestionTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act
            var command = manager.GetNextQuestion(quiz);

            // Assert
            var commandProceed = Assert.IsType<QuizFlowCommandProceedContract>(command);
            Assert.Equal(1, commandProceed.QuizId);
            Assert.Equal(2, commandProceed.Template.Answers.Count);
            Assert.DoesNotContain(commandProceed.Template.Answers, answer => answer.IsCorrect);
        }

        [Fact]
        public void GetNextQuestion_WhenQuizCompleted_ThrowsQuizFlow()
        {
            // Arrange
            var quizStub = new Quiz()
            {
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
            };
            var mockUow = new Mock<IUnitOfWork>();
            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act & Assert
            var ex = Assert.Throws<QuizFlowException>(() => manager.GetNextQuestion(quizStub));
            Assert.Equal(QuizFlowErrorCodes.QuizAlreadyCompleted.ToString(), ex.ErrorCode);
        }

        #endregion

        #region AnswerQuestion

        [Fact]
        public void AnswerQuestion_WhenQuestionAlreadyAnswered_ThrowsQuizFlow()
        {
            // Arrange
            var quizStub = new Quiz();
            var questionStub = new Question()
            {
                Id = 1,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
            };

            var mockUow = new Mock<IUnitOfWork>();
            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act & Assert
            var ex = Assert.Throws<QuizFlowException>(() => manager.AnswerQuestion(questionStub, 1));
            Assert.Equal(QuizFlowErrorCodes.QuestionAlreadyAnswered.ToString(), ex.ErrorCode);
        }

        [Fact]
        public void AnswerQuestion_WhenAnswerTemplateNotFound_ThrowsEntityNotFound()
        {
            // Arrange
            var questionStub = new Question();
            var questionTemplateStub = new QuestionTemplate();
            var mockQuizRepo = new Mock<IQuizRepository>();
            var mockQuestionTemplateRepo = new Mock<IQuestionTemplateRepository>();
            mockQuestionTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                    .Returns(value: questionTemplateStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuestionTemplateRepository)
                   .Returns(mockQuestionTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act & Assert
            var ex = Assert.Throws<EntityNotFoundException>(() => manager.AnswerQuestion(questionStub, 1));
            Assert.Equal("AnswerTemplate", ex.EntityType);
            Assert.Equal(1, ex.EntityId);
        }

        [Fact]
        public void AnswerQuestion_WhenAnswerTemplateFound_SetQuestionAnswered()
        {
            // Arrange
            var questionStub = new Question();
            var answerStub = new AnswerTemplate()
            {
                Id = 1,
                IsCorrect = true
            };
            var questionTemplateStub = new QuestionTemplate();
            questionTemplateStub.Answers.Add(answerStub);

            var mockQuizRepo = new Mock<IQuizRepository>();
            var mockQuestionTemplateRepo = new Mock<IQuestionTemplateRepository>();
            mockQuestionTemplateRepo.Setup(repo => repo.GetByID(It.IsAny<int>()))
                                    .Returns(value: questionTemplateStub);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(repo => repo.QuestionTemplateRepository)
                   .Returns(mockQuestionTemplateRepo.Object);

            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act
            manager.AnswerQuestion(questionStub, 1);

            // Assert
            Assert.True(questionStub.IsAnswered);
            Assert.Single(questionStub.Answers);
            Assert.True(questionStub.Answers.First().IsCorrect);
        }

        [Fact]
        public void AnswerQuestion_WhenQuizCompleted_ThrowsQuizFlow()
        {
            // Arrange
            var quizStub = new Quiz()
            {
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
            };
            var questionStub = new Question()
            {
                Quiz = quizStub
            };
            var mockUow = new Mock<IUnitOfWork>();
            var manager = new QuizFlowManager(mockUow.Object, null);

            // Act & Assert
            var ex = Assert.Throws<QuizFlowException>(() => manager.AnswerQuestion(questionStub, 1));
            Assert.Equal(QuizFlowErrorCodes.QuizAlreadyCompleted.ToString(), ex.ErrorCode);
        }

        #endregion

        #region CompleteQuiz

        [Fact]
        public void CompleteQuiz_WhenActiveQuiz_LaunchScoreCalculation()
        {
            // Arrange
            var quiz = new Quiz();
            var score = new Score();

            var mockScoreRepo = new Mock<IScoreRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.ScoreRepository).Returns(mockScoreRepo.Object);

            var mockScoreFactory = MockDefaultScoreCalculationFactory(score);

            var manager = new QuizFlowManager(mockUow.Object, mockScoreFactory.Object);

            // Act
            manager.CompleteQuiz(quiz);

            // Assert
            mockScoreFactory.Verify(f => f.CreateStrategy(quiz), Times.Once);
            mockScoreRepo.Verify(repo => repo.Insert(score), Times.Once);
            Assert.NotNull(quiz.DateEnd);
        }

        [Fact]
        public void CompleteQuiz_WhenQuizAlredyCompleted_ThrowsQuizFlow()
        {
            // Arrange
            var quiz = new Quiz()
            {
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
            };
            var score = new Score();

            var mockScoreRepo = new Mock<IScoreRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(uow => uow.ScoreRepository).Returns(mockScoreRepo.Object);

            var mockScoreFactory = MockDefaultScoreCalculationFactory(score);

            var manager = new QuizFlowManager(mockUow.Object, mockScoreFactory.Object);

            // Act & Assert
            var ex = Assert.Throws<QuizFlowException>(() => manager.CompleteQuiz(quiz));
            Assert.Equal(QuizFlowErrorCodes.QuizAlreadyCompleted.ToString(), ex.ErrorCode);
        }

        #endregion

        private Mock<IScoreCalculationFactory> MockDefaultScoreCalculationFactory(Score score)
        {
            var strategy = new Mock<IScoreCalculationStrategy>();
            strategy.Setup(s => s.CalculateScore(It.IsAny<Quiz>()))
                    .Returns(score);

            var mockScoreFactory = new Mock<IScoreCalculationFactory>();
            mockScoreFactory.Setup(f => f.CreateStrategy(It.IsAny<Quiz>()))
                            .Returns(strategy.Object);

            return mockScoreFactory;
        }
    }
}
