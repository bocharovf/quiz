using Microsoft.EntityFrameworkCore.Design;

namespace QuizService.DataAccess.Design
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDatabaseContext>
    {
        public ApplicationDatabaseContext CreateDbContext(string[] args)
        {
            return ApplicationDatabaseContextFactory.CreateContext("Host=localhost;Database=quiz");
        }
    }
}
