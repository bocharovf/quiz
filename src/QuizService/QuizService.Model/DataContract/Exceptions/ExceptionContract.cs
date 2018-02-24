using QuizService.Model.Exceptions;

namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Defines exception contract.
    /// </summary>
    public class ExceptionContract: IException
    {
        /// <summary>
        /// Gets code of error type.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Gets additional exception properties.
        /// </summary>
        public object Extension { get; }

        /// <summary>
        /// Gets displayed error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets correlation identifier.
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Initializes new instance of <see cref="ExceptionContract"/> from exception.
        /// </summary>
        /// <param name="exception">Exception contract.</param>
        /// <param name="correlationId">Correlation identifier.</param>
        public ExceptionContract(IException exception, string correlationId)
        {
            this.ErrorCode = exception.ErrorCode;
            this.Extension = exception.Extension;
            this.Message = exception.Message;

            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="ExceptionContract"/>.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="errorCode">Error code.</param>
        /// <param name="correlationId">Correlation identifier.</param>
        public ExceptionContract(string message, string errorCode, string correlationId)
        {
            this.Message = message;
            this.ErrorCode = errorCode;
            this.CorrelationId = correlationId;
        }

    }
}
