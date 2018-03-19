using Npgsql;
using System;

namespace QuizService.DataAccess.Configuration
{
    /// <summary>
    /// Provides methods to work with connection strings.
    /// </summary>
    internal class DatabaseConnectionStringBuilder
    {
        private const string DatabaseUserEnvironmentVariable = "POSTGRES_USER";
        private const string DatabasePasswordEnvironmentVariable = "POSTGRES_PASSWORD";

        /// <summary>
        /// Builds full database connection string with credentials using environment variables.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>Full database connection string with credentials.</returns>
        public static string BuildConnectionString(string connectionString)
        {
            var builder = new Npgsql​Connection​String​Builder(connectionString)
            {
                Username = Environment.GetEnvironmentVariable(DatabaseUserEnvironmentVariable),
                Password = Environment.GetEnvironmentVariable(DatabasePasswordEnvironmentVariable)
            };
            return builder.ConnectionString;
        }
    }
}
