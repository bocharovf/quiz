using QuizService.Model.DataContract;

namespace QuizService.Common.Logging
{
    /// <summary>
    /// Provides methods to format log messages.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Formats log message for client exception.
        /// </summary>
        /// <param name="exception">Client exception.</param>
        /// <returns>Formatted log message for client exception.</returns>
        string FormatClientException(ClientExceptionContract exception);

        /// <summary>
        /// Formats log message for http request.
        /// </summary>
        /// <param name="correlationId">Correlation identifier.</param>
        /// <param name="httpMethod">Http method.</param>
        /// <param name="requestedUrl">Requested resource URL.</param>
        /// <returns>Formatted log message for http request.</returns>
        string FormatHttpRequest(string correlationId, string httpMethod, string requestedUrl);

        /// <summary>
        /// Formats log message for http response.
        /// </summary>
        /// <param name="correlationId">Correlation identifier.</param>
        /// <param name="statusCode">Http response status code.</param>
        /// <param name="responseBody">Http response body.</param>
        /// <returns>Formatted log message for http response.</returns>
        string FormatHttpResponse(string correlationId, int statusCode, string responseBody);
    }
}