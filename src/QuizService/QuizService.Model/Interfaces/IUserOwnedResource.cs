namespace QuizService.Model.Interfaces
{
    /// <summary>
    /// Represents the resource access to that is limited by specific user.
    /// </summary>
    public interface IUserOwnedResource
    {
        /// <summary>
        /// Gets identifier of the user who owns the resource.
        /// </summary>
        /// <returns>Identifier of the resource owner user.</returns>
        int CreatedUserId { get; }
    }
}
