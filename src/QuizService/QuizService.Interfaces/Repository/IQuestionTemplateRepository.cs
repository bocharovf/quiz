using QuizService.Interfaces.Common;
using QuizService.Model;

namespace QuizService.Interfaces.Repository
{
    /// <summary>
    /// Repository interface for <see cref="QuestionTemplate"/>.
    /// </summary>
    public interface IQuestionTemplateRepository: IGenericRepository<QuestionTemplate, int>
    {
        /// <summary>
        /// Gets question template.
        /// </summary>
        /// <param name="quizTemplateId">The quiz template identifier.</param>
        /// <param name="nextQuestionOrder">Sequential number of question.</param>
        /// <returns>Question template.</returns>
        QuestionTemplate GetQuestionTemplate(int quizTemplateId, int nextQuestionOrder);
    }
}
