namespace QuizService.Model.Exceptions
{
    /// <summary>
    /// Represents application exception.
    /// </summary>
    /// <remarks>
    /// Abstracts exception inteface and removes dependency from standard Exception class
    /// which does not work well with Typescipt code generation.
    /// </remarks>
    public interface IException
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
