using QuizService.Model;

namespace QuizService.BusinessLogic.Scores
{
    public interface IScoreCalculationStrategy
    {
        Score CalculateScore(Quiz quiz);
    }
}