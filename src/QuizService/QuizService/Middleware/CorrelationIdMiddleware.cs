using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace QuizService.Middleware
{
    /// <summary>
    /// Middleware to get or create request correlation ID.
    /// </summary>
    public class CorrelationIdMiddleware
    {
        /// <summary>
        /// Http header name for correlation identifier.
        /// </summary>
        public const string CORRELATION_ID_HEADER = "X-Correlation-ID";

        private readonly RequestDelegate Next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (httpContext.Request.Headers.TryGetValue(CORRELATION_ID_HEADER, out StringValues correlationId))
            {
                httpContext.TraceIdentifier = correlationId;
            }
            else
            {
                httpContext.TraceIdentifier = GenerateCorrelationId();
            }

            httpContext.Response.OnStarting(() => {
                httpContext.Response.Headers.Add(CORRELATION_ID_HEADER, new[] { httpContext.TraceIdentifier });
                return Task.CompletedTask;
            });

            return this.Next(httpContext);
        }

        private string GenerateCorrelationId()
        {
            return String.Format("qs-{0}", Guid.NewGuid());
        }
    }
}
