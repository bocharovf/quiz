using QuizService.Model;

namespace QuizService.BusinessLogic.Scores
{
    /// <summary>
    /// <see cref="IScoreCalculationStrategy"/> factory.
    /// </summary>
    public class ScoreCalculationFactory : IScoreCalculationFactory
    {
        /// <summary>
        /// Creates appropriate instance of <see cref="IScoreCalculationStrategy"/>.
        /// </summary>
        /// <param name="quiz">Quiz to create strategy for.</param>
        /// <returns>Appropriate instance of <see cref="IScoreCalculationStrategy"/>.</returns>
        public IScoreCalculationStrategy CreateStrategy(Quiz quiz)
        {
            return new ScoreCalculationStrategyDefault();
        }
    }
}
