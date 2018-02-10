using QuizService.Interfaces.Common;
using QuizService.Model;

namespace QuizService.Interfaces.Repository
{
    /// <summary>
    /// Repository interface for <see cref="Score"/>.
    /// </summary>
    public interface IScoreRepository : IGenericRepository<Score, int>
    {
        /// <summary>
        /// Gets quiz scores.
        /// </summary>
        /// <param name="quizId">The quiz identifier.</param>
        /// <returns>Quiz scores.</returns>
        Score GetQuizScore(int quizId);
    }
}
