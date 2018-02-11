using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using System.Linq;

namespace QuizService.DataAccess.Repository
{
    internal class QuizRepository : GenericRepository<Quiz, int>, IQuizRepository
    {
        public QuizRepository(ApplicationDatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Quiz> IncludeProperties(IQueryable<Quiz> query)
        {
            var baseQuery = base.IncludeProperties(query);

            query = query.Include(quiz => quiz.Questions)
                         .ThenInclude(question => question.Answers);
            return query;
        }

        public override Quiz GetByID(int id)
        {
            return this.Get(quiz => quiz.Id == id).FirstOrDefault();
        }
    }
}
