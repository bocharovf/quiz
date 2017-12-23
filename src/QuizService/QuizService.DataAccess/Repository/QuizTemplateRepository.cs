using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess.Repository
{
    public class QuizTemplateRepository : GenericRepository<QuizTemplate, int>, IQuizTemplateRepository
    {
        internal QuizTemplateRepository(ApplicationDatabaseContext context) : base(context)
        {

        }

        public QuestionTemplate GetQuestionTemplate(int quizTemplateId, int order)
        {
            var quizQuestionTemplate = this.context.QuizQuestionTemplates
                                                    .Include(qqt => qqt.QuestionTemplate)
                                                        .ThenInclude(qt => qt.Answers)
                                                    .Where(qqt => 
                                                        qqt.QuizTemplateId == quizTemplateId && 
                                                        qqt.Order == order
                                                    )
                                                    .Select(qqt => qqt.QuestionTemplate)
                                                    .FirstOrDefault();

            return quizQuestionTemplate;
        }
    }
}
