using Microsoft.EntityFrameworkCore;

namespace QuizService.DataAccess.Configuration
{
    /// <summary>
    /// Provides <see cref="DbContextOptionsBuilder"/> extension methods.
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures application database context.
        /// </summary>
        /// <param name="builder">Database context builder.</param>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns>Database context builder.</returns>
        public static DbContextOptionsBuilder ConfigureApplcationContextOptions(this DbContextOptionsBuilder builder, string connectionString)
        {
            string completeConnectionString = DatabaseConnectionStringBuilder.BuildConnectionString(connectionString);
            builder.UseNpgsql(completeConnectionString);

            return builder;
        }
    }
}
