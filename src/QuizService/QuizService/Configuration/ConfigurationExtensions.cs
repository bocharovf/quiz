using Microsoft.Extensions.Configuration;

namespace QuizService
{
    /// <summary>
    /// Provides extension methods for <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        private const string DatabaseConnectionStringKey = "DatabaseConnectionString";

        private const string AdminEmailKey = "QUIZ_ADMIN_EMAIL";
        private const string DefaultAdminEmail = "admin@localhost";

        private const string AdminPasswordKey = "QUIZ_ADMIN_PASSWORD";
        private const string DefaultAdminPassword = "quiz";

        /// <summary>
        /// Gets database connection string.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Database connection string.</returns>
        public static string GetDatabaseConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(DatabaseConnectionStringKey);
        }

        /// <summary>
        /// Gets administrator email.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Administrator email.</returns>
        public static string GetAdministratorEmail(this IConfiguration configuration)
        {
            return configuration.GetValue<string>(AdminEmailKey, DefaultAdminEmail);
        }

        /// <summary>
        /// Gets administrator password.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Administrator password.</returns>
        public static string GetAdministratorPassword(this IConfiguration configuration)
        {
            return configuration.GetValue<string>(AdminPasswordKey, DefaultAdminPassword);
        }
    }
}
