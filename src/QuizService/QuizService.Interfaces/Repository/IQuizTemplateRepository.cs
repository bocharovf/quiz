using QuizService.Interfaces.Common;
using QuizService.Model;

namespace QuizService.Interfaces.Repository
{
    /// <summary>
    /// Repository interface for <see cref="QuizTemplate"/>.
    /// </summary>
    public interface IQuizTemplateRepository: IGenericRepository<QuizTemplate, int>
    {
        /// <summary>
        /// Gets the amount of questons in quiz template. 
        /// </summary>
        /// <param name="quizTemplateId">The quiz template identifier.</param>
        /// <returns>Amount of questions in quiz.</returns>
        int GetQuestionTemplateCount(int quizTemplateId);
    }
}
