using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuizService.Common.Logging;
using QuizService.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QuizService.Middleware
{
    /// <summary>
    /// Middleware to log requests and responses.
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly ILogger Logger;
        private readonly ILogFormatter LogFormatter;

        public RequestResponseLoggingMiddleware(
            RequestDelegate next, 
            ILogger<RequestResponseLoggingMiddleware> logger, 
            ILogFormatter logFormatter)
        {
            this.Next = next;
            this.Logger = logger;
            this.LogFormatter = logFormatter;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));            

            var originalBodyStream = httpContext.Response.Body;
            using (var temporaryBodyStream = new MemoryStream())
            {
                httpContext.Response.Body = temporaryBodyStream;

                await this.Next(httpContext);
                temporaryBodyStream.Seek(0, SeekOrigin.Begin);

                if (httpContext.Response.HasError())
                {
                    LogRequestResponse(httpContext);
                }                

                await temporaryBodyStream.CopyToAsync(originalBodyStream);
            }
        }

        private void LogRequestResponse(HttpContext httpContext)
        {
            string correlationId = httpContext.GetCorrelationId();
            HttpRequest request = httpContext.Request;
            HttpResponse response = httpContext.Response;

            string requestMessage = this.LogFormatter.FormatHttpRequest(correlationId, request.Method, request.GetRequestUrl());
            this.Logger.LogError(ApplicationLogEvents.IncomingRequest, requestMessage);

            string responseMessage = this.LogFormatter.FormatHttpResponse(correlationId, response.StatusCode, GetResponseBodyText(response));
            this.Logger.LogError(ApplicationLogEvents.RequestError, responseMessage);
        }

        private static string GetResponseBodyText(HttpResponse response)
        {
            string responseBody = new StreamReader(response.Body).ReadToEnd();
            response.Body.Seek(0, SeekOrigin.Begin);
            return responseBody;
        }
    }
}
