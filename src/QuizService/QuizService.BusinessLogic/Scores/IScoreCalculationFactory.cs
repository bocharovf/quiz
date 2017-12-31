using QuizService.Model;

namespace QuizService.BusinessLogic.Scores
{
    public interface IScoreCalculationFactory
    {
        IScoreCalculationStrategy CreateStrategy(Quiz quiz);
    }
}