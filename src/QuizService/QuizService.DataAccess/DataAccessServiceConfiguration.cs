using Microsoft.Extensions.DependencyInjection;
using QuizService.DataAccess.Common;
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
            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>(
                (provider) => new UnitOfWorkFactory(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>((provider) => {
                var context = ApplicationDatabaseContextFactory.CreateContext(connectionString);
                return new UnitOfWork(context);
            });
        }
    }
}
