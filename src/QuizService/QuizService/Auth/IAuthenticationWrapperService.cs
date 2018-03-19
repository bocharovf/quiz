using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizService.DataAccess.Auth;
using QuizService.Model;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides authecation methods.
    /// </summary>
    public interface IAuthenticationWrapperService
    {
        Task SignOutAsync();

        Task<SignInResult> PasswordSignInAsync(string login, string password, bool remember, bool lockoutOnFailure);

        Task<IdentityResult> AddToRoleAsync(AspnetUser user, string role);

        Task<IdentityResult> CreateAsync(AspnetUser user, string password);

        Task SignInAsync(AspnetUser user, bool isPersistent);

        /// <summary>
        /// Gets domain user from principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>Domain user.</returns>
        Task<User> GetDomainUserAsync(ClaimsPrincipal principal);

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        bool IsAuthenticated(ClaimsPrincipal principal);
    }
}