using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizService.BusinessLogic.QuizFlow;
using QuizService.BusinessLogic.Scores;
using QuizService.DataAccess;
using QuizService.Interfaces.Managers;

namespace QuizService
{
    /// <summary>
    /// Provides methods for application services configuration.
    /// </summary>
    public class ServiceConfiguration
    {
        /// <summary>
        /// Configures application services.
        /// </summary>
        /// <param name="services">Application services collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetDatabaseConnectionString();

            DataAccessServiceConfiguration.ConfigureServices(services, databaseConnectionString);

            services.AddTransient<IQuizFlowManager, QuizFlowManager>();
            services.AddTransient<IScoreCalculationFactory, ScoreCalculationFactory>();
        }
    }
}
