using QuizService.Model.DataContract;

namespace QuizService.Common.Logging
{
    /// <summary>
    /// Provides methods to format log messages.
    /// </summary>
    public class LogFormatter : ILogFormatter
    {
        /// <summary>
        /// Formats log message for client exception.
        /// </summary>
        /// <param name="exception">Client exception.</param>
        /// <returns>Formatted log message for client exception.</returns>
        public string FormatClientException(ClientExceptionContract exception)
        {
            return $"{exception.CorrelationId}: CLIENT {exception.ErrorCode} - {exception.Message} at {exception.StackTrace} ({exception.ClientPlatform})";
        }

        /// <summary>
        /// Formats log message for http request.
        /// </summary>
        /// <param name="correlationId">Correlation identifier.</param>
        /// <param name="httpMethod">Http method.</param>
        /// <param name="requestedUrl">Requested resource URL.</param>
        /// <returns>Formatted log message for http request.</returns>
        public string FormatHttpRequest(string correlationId, string httpMethod, string requestedUrl)
        {
            return $"{correlationId}: REQUEST {httpMethod} {requestedUrl}";
        }

        /// <summary>
        /// Formats log message for http response.
        /// </summary>
        /// <param name="correlationId">Correlation identifier.</param>
        /// <param name="statusCode">Http response status code.</param>
        /// <param name="responseBody">Http response body.</param>
        /// <returns>Formatted log message for http response.</returns>
        public string FormatHttpResponse(string correlationId, int statusCode, string responseBody)
        {
            return $"{correlationId}: RESPONSE {statusCode} {responseBody}";
        }
    }
}
