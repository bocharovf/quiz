using Microsoft.AspNetCore.Identity;
using QuizService.DataAccess.Auth;
using System.Threading.Tasks;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides authentication methods.
    /// </summary>
    public interface IAuthenticationWrapperService
    {
        Task SignOutAsync();

        Task<SignInResult> PasswordSignInAsync(string login, string password, bool remember, bool lockoutOnFailure);

        Task<IdentityResult> AddToRoleAsync(AspnetUser user, string role);

        Task<IdentityResult> CreateAsync(AspnetUser user, string password);

        Task SignInAsync(AspnetUser user, bool isPersistent);
    }
}