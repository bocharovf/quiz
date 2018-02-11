namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Defines service exception.
    /// </summary>
    public interface IServiceExceptionContract
    {
        /// <summary>
        /// Gets code of error type.
        /// </summary>
        string ErrorCode { get; }

        /// <summary>
        /// Gets additional exception properties.
        /// </summary>
        object Extension { get; }

        /// <summary>
        /// Gets displayed error message.
        /// </summary>
        string Message { get; }
    }
}