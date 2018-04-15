using Microsoft.EntityFrameworkCore;
using QuizService.DataAccess.Configuration;

namespace QuizService.DataAccess
{
    /// <summary>
    /// Provides methods to create <see cref="ApplicationDatabaseContext"/> instance.
    /// </summary>
    internal class ApplicationDatabaseContextFactory
    {
        /// <summary>
        /// Creates and configures <see cref="ApplicationDatabaseContext"/>.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ApplicationDatabaseContext CreateContext(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDatabaseContext>()
                                                .ConfigureApplcationContextOptions(connectionString);

            return new ApplicationDatabaseContext(dbContextOptionsBuilder.Options);
        }
    }
}
