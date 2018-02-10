namespace QuizService.Model.Exceptions
{
    /// <summary>
    /// Defines an exception related to application business logic.
    /// </summary>
    /// <remarks>
    /// The interface replaces base enitity for Typescript code generation.
    /// Using custom interface instead of Exception solves a lot of codegeneration problem 
    /// and simplifies client model.
    /// </remarks>
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