using QuizService.Model;

namespace QuizService.BusinessLogic.Scores
{
    public class ScoreCalculationFactory : IScoreCalculationFactory
    {
        public IScoreCalculationStrategy CreateStrategy(Quiz quiz)
        {
            return new ScoreCalculationStrategyDefault();
        }
    }
}
