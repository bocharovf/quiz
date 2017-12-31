using QuizService.Model;
using System.Linq;

namespace QuizService.BusinessLogic.Scores
{
    public class ScoreCalculationStrategyDefault : IScoreCalculationStrategy
    {
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
