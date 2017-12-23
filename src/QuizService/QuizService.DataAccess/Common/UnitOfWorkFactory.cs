using QuizService.Interfaces.Common;

namespace QuizService.DataAccess.Common
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private string ConnectionString;

        public UnitOfWorkFactory(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var context = ApplicationDatabaseContextFactory.CreateContext(this.ConnectionString);
            return new UnitOfWork(context);
        }
    }
}
