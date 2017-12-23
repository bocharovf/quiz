using Microsoft.Extensions.Configuration;

namespace QuizService
{
    public static class ConfigurationExtensions
    {
        private const string DatabaseConnectionString = "DatabaseConnectionString";

        public static string GetDatabaseConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(DatabaseConnectionString);
        }
    }
}
