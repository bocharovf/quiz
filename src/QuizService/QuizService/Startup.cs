using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizService.Configuration;
using QuizService.DataAccess;
using QuizService.DataAccess.Auth;
using QuizService.Middleware;

namespace QuizService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors()
                    .AddMvc()
                    .AddJsonOptions(JsonSerializationConfiguration.Setup);

            ServiceConfiguration.ConfigureServices(services, this.Configuration);
            ServiceConfiguration.ConfigureAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            UserManager<AspnetUser> userManager, RoleManager<AspnetRole> rolerManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCors()
               // disable after migration to .net core 2.1 due to bug
               // https://github.com/aspnet/AspNetCore/issues/5144
               // TODO: consider to use OpenTracing instead of custom middleware
               //.UseCorrelationId()
               .UseAuthentication()
               .UseRequestLogging()
               .UseMvc();

            string databaseConnectionString = this.Configuration.GetDatabaseConnectionString();
            string administratorEmail = this.Configuration.GetAdministratorEmail();
            string administratorPassword = this.Configuration.GetAdministratorPassword();
            DatabaseInitializer.InitializeDatabase(
                databaseConnectionString, administratorEmail, administratorPassword, 
                userManager, rolerManager).Wait();
        }
    }
}
