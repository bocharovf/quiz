using QuizService.Model;

namespace QuizService.Interfaces.Services
{
    /// <summary>
    /// Provides access to the current domain user.
    /// </summary>
    public interface IUserAccessorService
    {
        /// <summary>
        /// Gets current domain user.
        /// </summary>
        User DomainUser { get; }

        /// <summary>
        /// Gets a value that indicates whether the current user has been authenticated.
        /// </summary>
        /// <returns>true if the current user was authenticated; otherwise, false.</returns>
        bool IsAuthenticated { get; }
    }
}
