using Microsoft.AspNetCore.Builder;
using QuizService.Middleware;

namespace QuizService.Configuration
{
    public static class CorsConfiguration
    {
        /// <summary>
        /// Configures CORS parameters.
        /// </summary>
        /// <param name="builder">Application builder.</param>
        /// <returns>Application builder.</returns>
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder builder)
        {
            return builder.UseCors(options => 
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(CorrelationIdMiddleware.CORRELATION_ID_HEADER));
        }
    }
}
