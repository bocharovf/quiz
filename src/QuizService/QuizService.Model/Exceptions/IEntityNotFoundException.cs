namespace QuizService.Model.Exceptions
{
    /// <summary>
    /// Defines an exception that occured when entity is not found.
    /// </summary>
    public interface IEntityNotFoundException: IBusinessLogicException
    {
        /// <summary>
        /// Gets entity identifier.
        /// </summary>
        object EntityId { get; }

        /// <summary>
        /// Gets entity type name.
        /// </summary>
        string EntityType { get; }
    }
}