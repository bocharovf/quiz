using QuizService.Common.Logging;
using QuizService.Model.DataContract;
using System;
using Xunit;

namespace QuizService.Common.UnitTests
{
    public class LogFormatterTests
    {
        [Fact]
        public void FormatClientException_Always_ReturnsCorretStringRepresentation()
        {
            var formatter = new LogFormatter();
            var exception = new ClientExceptionContract() {
                ClientPlatform = "Chrome",
                CorrelationId = "qc-123",
                ErrorCode = "UNEXPECTED",
                Message = "Test error",
                StackTrace = "TestModule.MyFunc"
            };

            string formattedMessage = formatter.FormatClientException(exception);

            Assert.Equal("qc-123: CLIENT UNEXPECTED - Test error at TestModule.MyFunc (Chrome)", formattedMessage);
        }

        [Fact]
        public void FormatHttpRequest_Always_ReturnsCorretStringRepresentation()
        {
            var formatter = new LogFormatter();

            string formattedMessage = formatter.FormatHttpRequest("qc-123", "GET", "https://domain.com/resource?x=1");

            Assert.Equal("qc-123: REQUEST GET https://domain.com/resource?x=1", formattedMessage);
        }

        [Fact]
        public void FormatHttpResponse_Always_ReturnsCorretStringRepresentation()
        {
            var formatter = new LogFormatter();

            string formattedMessage = formatter.FormatHttpResponse("qc-123", 500, "Error json");

            Assert.Equal("qc-123: RESPONSE 500 Error json", formattedMessage);
        }
    }
}
