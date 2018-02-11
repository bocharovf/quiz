using Microsoft.EntityFrameworkCore.Design;

namespace QuizService.DataAccess.Design
{
    /// <summary>
    /// Design time database context factory for migrations support.
    /// </summary>
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDatabaseContext>
    {
        public ApplicationDatabaseContext CreateDbContext(string[] args)
        {
            return ApplicationDatabaseContextFactory.CreateContext("Host=localhost;Database=quiz");
        }
    }
}
