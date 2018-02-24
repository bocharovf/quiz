using Microsoft.AspNetCore.Builder;
using QuizService.BusinessLogic;

namespace QuizService.Middleware
{
    /// <summary>
    /// Provides extension methods for middleware registration.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Registers <see cref="CorrelationIdMiddleware"/>.
        /// </summary>
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            ThrowIf.Null(app, nameof(app));

            return app.UseMiddleware<CorrelationIdMiddleware>();
        }

        /// <summary>
        /// Registers <see cref="RequestResponseLoggingMiddleware"/>.
        /// </summary>
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            ThrowIf.Null(app, nameof(app));

            return app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
        
    }
}
