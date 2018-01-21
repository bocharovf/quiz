using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess.Repository
{
    public class ScoreRepository : GenericRepository<Score, int>, IScoreRepository
    {
        internal ScoreRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        public Score GetQuizScore(int quizId)
        {
            return this.context.Scores.FirstOrDefault(score => score.QuizId == quizId);
        }
    }
}
