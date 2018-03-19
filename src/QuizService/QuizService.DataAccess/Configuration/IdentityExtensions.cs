using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace QuizService.DataAccess.Configuration
{
    /// <summary>
    /// Provides <see cref="IdentityBuilder"/> extension methods.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Adds application identity store.
        /// </summary>
        /// <param name="builder">Identity builder.</param>
        /// <returns>Identity builder.</returns>
        public static IdentityBuilder AddApplicationIdentityStore(this IdentityBuilder builder)
        {
            return builder.AddEntityFrameworkStores<ApplicationDatabaseContext>();
        }
    }
}
