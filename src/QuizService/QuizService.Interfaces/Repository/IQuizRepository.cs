using QuizService.Interfaces.Common;
using QuizService.Model;

namespace QuizService.Interfaces.Repository
{
    /// <summary>
    /// Repository interface for <see cref="Quiz"/>.
    /// </summary>
    public interface IQuizRepository: IGenericRepository<Quiz, int>
    {

    }
}
