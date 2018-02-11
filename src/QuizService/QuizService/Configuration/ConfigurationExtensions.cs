using Microsoft.Extensions.Configuration;

namespace QuizService
{
    /// <summary>
    /// Provides extension methods for <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        private const string DatabaseConnectionString = "DatabaseConnectionString";

        /// <summary>
        /// Gets database connection string.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Database connection string.</returns>
        public static string GetDatabaseConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(DatabaseConnectionString);
        }
    }
}
