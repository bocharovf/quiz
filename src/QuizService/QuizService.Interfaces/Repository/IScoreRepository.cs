using QuizService.Interfaces.Common;
using QuizService.Model;
using System;

namespace QuizService.Interfaces.Repository
{
    public interface IScoreRepository : IGenericRepository<Score, int>
    {
        Score GetQuizScore(int quizId);
    }
}
