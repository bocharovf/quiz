using Microsoft.EntityFrameworkCore;
using QuizService.Interfaces.Repository;
using QuizService.Interfaces.Services;
using QuizService.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QuizService.DataAccess.Repository
{
    internal class QuizRepository : GenericRepository<Quiz, int>, IQuizRepository
    {
        private readonly IAccessControlService accessControl;

        public QuizRepository(ApplicationDatabaseContext context,
            IAccessControlService accessControl) : base(context)
        {
            this.accessControl = accessControl;
        }

        protected override Expression<Func<Quiz, bool>> GlobalFilter {
            get => accessControl.GetAccessExpression<Quiz>();
        }

        protected override IQueryable<Quiz> IncludeProperties(IQueryable<Quiz> query)
        {
            var baseQuery = base.IncludeProperties(query);

            query = query.Include(quiz => quiz.Questions)
                         .ThenInclude(question => question.Answers);
            return query;
        }

        /// <summary>
        /// Gets quiz by identifier.
        /// </summary>
        /// <param name="id">Quiz identifier.</param>
        /// <returns>The quiz.</returns>
        /// <remarks>Overridden to use included properties.</remarks>
        public override Quiz GetByID(int id)
        {
            return this.Get(quiz => quiz.Id == id).FirstOrDefault();
        }
    }
}
