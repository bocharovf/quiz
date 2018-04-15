using QuizService.Interfaces.Repository;
using QuizService.Interfaces.Services;
using QuizService.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QuizService.DataAccess.Repository
{
    internal class ScoreRepository : GenericRepository<Score, int>, IScoreRepository
    {
        private readonly IAccessControlService accessControl;

        public ScoreRepository(ApplicationDatabaseContext context,
            IAccessControlService accessControl) : base(context)
        {
            this.accessControl = accessControl;
        }

        protected override Expression<Func<Score, bool>> GlobalFilter
        {
            get => accessControl.GetAccessExpression<Score>();
        }

        public Score GetQuizScore(int quizId)
        {
            return this.Collection.FirstOrDefault(score => score.QuizId == quizId);
        }
    }
}
