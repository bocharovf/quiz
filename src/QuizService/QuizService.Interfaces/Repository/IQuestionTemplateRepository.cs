using QuizService.Interfaces.Common;
using QuizService.Model;

namespace QuizService.Interfaces.Repository
{
    public interface IQuestionTemplateRepository: IGenericRepository<QuestionTemplate, int>
    {
        QuestionTemplate GetQuestionTemplate(int quizTemplateId, int nextQuestionOrder);
    }
}
