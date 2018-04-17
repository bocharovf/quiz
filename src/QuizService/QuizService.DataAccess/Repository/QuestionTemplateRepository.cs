using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess.Repository
{
    internal class QuestionTemplateRepository : GenericRepository<QuestionTemplate, int>, IQuestionTemplateRepository
    {
        public QuestionTemplateRepository(ApplicationDatabaseContext context) : base(context)
        {

        }

        protected override IQueryable<QuestionTemplate> IncludeProperties(IQueryable<QuestionTemplate> query)
        {
            var baseQuery = base.IncludeProperties(query);

            query = query.Include(quiz => quiz.Answers);
            return query;
        }

        /// <summary>
        /// Gets question template by identifier.
        /// </summary>
        /// <param name="id">Question template identifier.</param>
        /// <returns>The question template.</returns>
        /// <remarks>Overridden to use included properties.</remarks>
        public override QuestionTemplate GetByID(int id)
        {
            return this.Get(question => question.Id == id).FirstOrDefault();
        }

        public QuestionTemplate GetQuestionTemplate(int quizTemplateId, int order)
        {
            var quizQuestionTemplate = this.context.QuizQuestionTemplates
                                                    .Where(qqt => 
                                                        qqt.QuizTemplateId == quizTemplateId && 
                                                        qqt.Order == order
                                                    )
                                                    .Select(qqt => qqt.QuestionTemplate)
                                                    .Include(qt => qt.Answers)
                                                    .FirstOrDefault();

            return quizQuestionTemplate;
        }
    }
}
