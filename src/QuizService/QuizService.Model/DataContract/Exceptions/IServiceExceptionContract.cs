namespace QuizService.Model.DataContract
{
    public interface IServiceExceptionContract
    {
        string ErrorCode { get; }
        object Extension { get; }
        string Message { get; }
    }
}