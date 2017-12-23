using Microsoft.Extensions.DependencyInjection;
using QuizService.DataAccess.Common;
using QuizService.Interfaces.Common;

namespace QuizService.DataAccess
{
    public class DataAccessServiceConfiguration
    {
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
