using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizService.BusinessLogic.QuizFlow;
using QuizService.DataAccess;
using QuizService.Interfaces.Managers;

namespace QuizService
{
    public class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetDatabaseConnectionString();

            DataAccessServiceConfiguration.ConfigureServices(services, databaseConnectionString);

            services.AddTransient<IQuizFlowManager, QuizFlowManager>();
        }
    }
}
