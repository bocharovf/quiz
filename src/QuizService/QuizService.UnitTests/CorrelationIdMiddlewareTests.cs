using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using QuizService.Middleware;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QuizService.UnitTests
{
    public class CorrelationIdMiddlewareTests
    {
        [Fact]
        public void Invoke_WhenCorrelationIdHeaderExists_UsesHeaderValueAsTraceIdentifier()
        {
            // Arrange
            var requestHeaders = new Dictionary<string, StringValues>
            {
                { "X-Correlation-ID", "qc-c526d466-c817-4db6-9bd7-588a89c0b1c4" }
            };
            var responseHeaders = new Dictionary<string, StringValues>();

            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(req => req.Headers).Returns(new HeaderDictionary(requestHeaders));

            var httpResponse = new Mock<HttpResponse>();
            httpResponse.Setup(res => res.Headers).Returns(new HeaderDictionary(responseHeaders));

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(ctx => ctx.Request).Returns(httpRequest.Object);
            httpContext.Setup(ctx => ctx.Response).Returns(httpResponse.Object);
            httpContext.SetupProperty(ctx => ctx.TraceIdentifier);

            var middleware = new CorrelationIdMiddleware((ctx) => Task.CompletedTask);

            // Act
            var response = middleware.Invoke(httpContext.Object);

            // Assert
            httpContext.VerifySet(ctx => ctx.TraceIdentifier = "qc-c526d466-c817-4db6-9bd7-588a89c0b1c4");
        }

        [Fact]
        public void Invoke_WhenCorrelationIdHeaderMissing_UsesGeneratesTraceIdentifier()
        {
            // Arrange
            var requestHeaders = new Dictionary<string, StringValues>();
            var responseHeaders = new Dictionary<string, StringValues>();

            var httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(req => req.Headers).Returns(new HeaderDictionary(requestHeaders));

            var httpResponse = new Mock<HttpResponse>();
            httpResponse.Setup(res => res.Headers).Returns(new HeaderDictionary(responseHeaders));

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(ctx => ctx.Request).Returns(httpRequest.Object);
            httpContext.Setup(ctx => ctx.Response).Returns(httpResponse.Object);
            httpContext.SetupProperty(ctx => ctx.TraceIdentifier);

            var middleware = new CorrelationIdMiddleware((ctx) => Task.CompletedTask);

            // Act
            var response = middleware.Invoke(httpContext.Object);

            // Assert
            Assert.StartsWith("qs-", httpContext.Object.TraceIdentifier);
        }
    }
}
