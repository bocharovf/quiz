using Microsoft.AspNetCore.Http;

namespace QuizService.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets request correlation identifier.
        /// </summary>
        /// <param name="httpContext">Http context.</param>
        /// <returns>Correlation identifier.</returns>
        public static string GetCorrelationId(this HttpContext httpContext) => httpContext.TraceIdentifier;
    }
}
