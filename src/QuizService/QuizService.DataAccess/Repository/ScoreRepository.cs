using QuizService.Interfaces.Repository;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess.Repository
{
    internal class ScoreRepository : GenericRepository<Score, int>, IScoreRepository
    {
        public ScoreRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public Score GetQuizScore(int quizId)
        {
            return this.context.Scores.FirstOrDefault(score => score.QuizId == quizId);
        }
    }
}
