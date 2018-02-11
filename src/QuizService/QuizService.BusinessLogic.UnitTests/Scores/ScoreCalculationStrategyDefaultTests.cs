using QuizService.BusinessLogic.Scores;
using QuizService.Common.Extensions;
using QuizService.Model;
using Xunit;

namespace QuizService.BusinessLogic.UnitTests
{
    public class ScoreCalculationStrategyDefaultTests
    {
        [Fact]
        public void CalculateScore_WhenMultipleQuestionsHasMultipleAnswers_ExpectsCorrectScoreAmount()
        {
            // Arrange
            var answerq1a1 = new Answer()
            {
                IsCorrect = true
            };
            var answerq1a2 = new Answer()
            {
                IsCorrect = false
            };
            var answerq1a3 = new Answer()
            {
                IsCorrect = true
            };
            var question1 = new Question();
            question1.Answers.AddRange(new[] { answerq1a1, answerq1a2, answerq1a3 });

            var answerq2a1 = new Answer()
            {
                IsCorrect = false
            };
            var question2 = new Question();
            question2.Answers.Add(answerq2a1);

            var answerq3a1 = new Answer()
            {
                IsCorrect = true
            };
            var question3 = new Question();
            question3.Answers.Add(answerq3a1);

            var quiz = new Quiz()
            {
                Id = 1
            };
            quiz.Questions.AddRange(new[] { question1, question2, question3 });

            var strategy = new ScoreCalculationStrategyDefault();

            // Act
            var score = strategy.CalculateScore(quiz);

            // Assert
            Assert.Equal(3, score.ScoresAmount);
            Assert.Equal(1, score.QuizId);
        }
    }
}
