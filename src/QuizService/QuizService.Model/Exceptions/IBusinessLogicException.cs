namespace QuizService.Model.Exceptions
{
    public interface IBusinessLogicException
    {
        string ErrorCode { get; }
        object Extension { get; }
        string Message { get; }
    }
}