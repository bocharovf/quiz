namespace QuizService.Model.Exceptions
{
    public interface IEntityNotFoundException: IBusinessLogicException
    {
        object EntityId { get; }
        string EntityType { get; }
    }
}