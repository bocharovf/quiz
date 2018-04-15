using QuizService.DataAccess.Repository;
using QuizService.Interfaces.Common;
using QuizService.Interfaces.Repository;
using QuizService.Interfaces.Services;
using System;

namespace QuizService.DataAccess.Common
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDatabaseContext Context;
        private readonly IAccessControlService AccessControl;

        public UnitOfWork(ApplicationDatabaseContext context, IAccessControlService accessControlService)
        {
            this.Context = context;
            this.AccessControl = accessControlService;
        }

        public IQuizRepository QuizRepository => new QuizRepository(Context, AccessControl);
        public IQuizTemplateRepository QuizTemplateRepository => new QuizTemplateRepository(Context);
        public IQuestionTemplateRepository QuestionTemplateRepository => new QuestionTemplateRepository(Context);
        public IScoreRepository ScoreRepository => new ScoreRepository(Context, AccessControl);

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
