using Microsoft.Extensions.DependencyInjection;
using QuizService.DataAccess.Common;
using QuizService.DataAccess.Configuration;
using QuizService.Interfaces.Common;

namespace QuizService.DataAccess
{
    /// <summary>
    /// Contains configuration settings for data access related services.
    /// </summary>
    public class DataAccessServiceConfiguration
    {
        /// <summary>
        /// Configures data access settings.
        /// </summary>
        /// <param name="services">The collection of application services.</param>
        /// <param name="connectionString">The database connection string.</param>
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDatabaseContext>(options =>
                options.ConfigureApplcationContextOptions(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
