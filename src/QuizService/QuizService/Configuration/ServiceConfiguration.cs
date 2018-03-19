using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizService.Auth;
using QuizService.BusinessLogic.QuizFlow;
using QuizService.BusinessLogic.Scores;
using QuizService.Common.Logging;
using QuizService.DataAccess;
using QuizService.DataAccess.Auth;
using QuizService.DataAccess.Configuration;
using QuizService.Interfaces.Managers;
using System;
using System.Threading.Tasks;

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

            services.AddSingleton<ILogFormatter, LogFormatter>();
            services.AddTransient<IQuizFlowManager, QuizFlowManager>();
            services.AddTransient<IScoreCalculationFactory, ScoreCalculationFactory>();
            services.AddTransient<IAuthenticationWrapperService, AuthenticationWrapperService>();
        }

        /// <summary>
        /// Configures authentication.
        /// </summary>
        /// <param name="services">Application services collection.</param>
        public static void ConfigureAuthentication(IServiceCollection services) {
            IdentityBuilder identityBuilder = services.AddIdentity<AspnetUser, AspnetRole>()
                                                .AddApplicationIdentityStore()
                                                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;

                options.Events.OnRedirectToLogin = ReturnForbidden;
                options.Events.OnRedirectToAccessDenied = ReturnForbidden;
            });
        }

        private static Task ReturnForbidden(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }
    }
}
