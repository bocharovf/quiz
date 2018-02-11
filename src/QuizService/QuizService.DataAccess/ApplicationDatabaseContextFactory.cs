using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace QuizService.DataAccess
{
    internal class ApplicationDatabaseContextFactory
    {
        private const string DatabaseUserEnvironmentVariable = "POSTGRES_USER";
        private const string DatabasePasswordEnvironmentVariable = "POSTGRES_PASSWORD";

        public static ApplicationDatabaseContext CreateContext(string connectionString)
        {
            string completeConnectionString = BuildConnectionString(connectionString);
            DbContextOptions<ApplicationDatabaseContext> options = GetDbContextOptions(completeConnectionString);
            return new ApplicationDatabaseContext(options);
        }

        private static string BuildConnectionString(string connectionString)
        {
            var builder = new Npgsql​Connection​String​Builder(connectionString)
            {
                Username = Environment.GetEnvironmentVariable(DatabaseUserEnvironmentVariable),
                Password = Environment.GetEnvironmentVariable(DatabasePasswordEnvironmentVariable)
            };
            return builder.ConnectionString;
        }

        private static DbContextOptions<ApplicationDatabaseContext> GetDbContextOptions(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDatabaseContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);
            return dbContextOptionsBuilder.Options;
        }
    }
}
