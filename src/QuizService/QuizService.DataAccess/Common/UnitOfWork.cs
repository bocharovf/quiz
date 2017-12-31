using QuizService.DataAccess.Repository;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Repository;
using QuizService.Model;
using System;

namespace QuizService.DataAccess.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDatabaseContext Context;

        internal UnitOfWork(ApplicationDatabaseContext context)
        {
            this.Context = context;
        }

        public IQuizRepository QuizRepository => new QuizRepository(Context);
        public IQuizTemplateRepository QuizTemplateRepository => new QuizTemplateRepository(Context);
        public IQuestionTemplateRepository QuestionTemplateRepository => new QuestionTemplateRepository(Context);
        public IScoreRepository ScoreRepository => new ScoreRepository(Context);

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
