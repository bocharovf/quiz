using QuizService.Model;
using System.Linq;

namespace QuizService.BusinessLogic.Scores
{
    /// <summary>
    /// Default strategy for score calculation.
    /// </summary>
    public class ScoreCalculationStrategyDefault : IScoreCalculationStrategy
    {
        /// <summary>
        /// Calculates scores for specified quiz as an amount of correct answers.
        /// </summary>
        /// <param name="quiz">The quiz to calculate scores for.</param>
        /// <returns>Score for the quiz.</returns>
        public Score CalculateScore(Quiz quiz)
        {
            var scoresAmount = quiz.Questions
                                   .SelectMany(q => q.Answers)
                                   .Count(answer => answer.IsCorrect);

            return new Score()
            {
                QuizId = quiz.Id,
                ScoresAmount = scoresAmount
            };
        }
    }
}
