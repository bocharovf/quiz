namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Default service exception contract for unexpected errors.
    /// </summary>
    public class DefaultServiceExceptionContract : IServiceExceptionContract
    {
        public string ErrorCode => "InternalServerError";

        public object Extension => null;

        public string Message => "Unexpected server error.";
    }
}
