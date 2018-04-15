using QuizService.Interfaces.Common;
using QuizService.Interfaces.Services;

namespace QuizService.DataAccess.Common
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string ConnectionString;
        private readonly IAccessControlService AccessControl;

        public UnitOfWorkFactory(string connectionString, IAccessControlService accessControl)
        {
            this.ConnectionString = connectionString;
            this.AccessControl = accessControl;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var context = ApplicationDatabaseContextFactory.CreateContext(this.ConnectionString);
            return new UnitOfWork(context, this.AccessControl);
        }
    }
}
