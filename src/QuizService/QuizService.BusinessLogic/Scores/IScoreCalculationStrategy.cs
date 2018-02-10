using QuizService.Model;

namespace QuizService.BusinessLogic.Scores
{
    /// <summary>
    /// Score calculation strategy.
    /// </summary>
    public interface IScoreCalculationStrategy
    {
        /// <summary>
        /// Calculates scores for specified quiz.
        /// </summary>
        /// <param name="quiz">The quiz to calculate scores for.</param>
        /// <returns>Score for the quiz.</returns>
        Score CalculateScore(Quiz quiz);
    }
}