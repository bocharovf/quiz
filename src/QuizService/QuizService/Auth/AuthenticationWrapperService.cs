using Microsoft.AspNetCore.Identity;
using QuizService.DataAccess.Auth;
using System.Threading.Tasks;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides authentication methods.
    /// </summary>
    /// <remarks>Wrapping built-in ASP.NET Identity services simplifies unit testing.</remarks>
    public class AuthenticationWrapperService : IAuthenticationWrapperService
    {
        private readonly UserManager<AspnetUser> UserManager;
        private readonly SignInManager<AspnetUser> SignInManager;

        public AuthenticationWrapperService(
            UserManager<AspnetUser> userManager, 
            SignInManager<AspnetUser> signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        #region Wrapped methods

        public async Task SignOutAsync()
        {
            await this.SignInManager.SignOutAsync();
        }

        public async Task<SignInResult> PasswordSignInAsync(string login, string password, bool remember, bool lockoutOnFailure)
        {
            return await this.SignInManager.PasswordSignInAsync(login, password, remember, lockoutOnFailure);
        }

        public async Task<IdentityResult> AddToRoleAsync(AspnetUser user, string role)
        {
            return await this.UserManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateAsync(AspnetUser user, string password)
        {
            return await this.UserManager.CreateAsync(user, password);
        }

        public async Task SignInAsync(AspnetUser user, bool isPersistent)
        {
            await this.SignInManager.SignInAsync(user, isPersistent);
        }

        #endregion

    }
}
