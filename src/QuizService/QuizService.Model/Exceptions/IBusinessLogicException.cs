namespace QuizService.Model.Exceptions
{
    /// <summary>
    /// Defines an exception related to application business logic.
    /// </summary>
    public interface IBusinessLogicException
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