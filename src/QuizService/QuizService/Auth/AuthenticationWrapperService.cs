using Microsoft.AspNetCore.Identity;
using QuizService.BusinessLogic;
using QuizService.DataAccess.Auth;
using QuizService.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides authecation methods.
    /// </summary>
    /// <remarks>Wrapping built-in ASP.NET Identity services simplifies unit testing.</remarks>
    public class AuthenticationWrapperService : IAuthenticationWrapperService
    {
        private readonly UserManager<AspnetUser> UserManager;
        private readonly SignInManager<AspnetUser> SignInManager;

        public AuthenticationWrapperService(UserManager<AspnetUser> userManager, SignInManager<AspnetUser> signInManager)
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

        /// <summary>
        /// Gets domain user from principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>Domain user.</returns>
        public async Task<User> GetDomainUserAsync(ClaimsPrincipal principal)
        {
            ThrowIf.Null(principal, nameof(principal));

            AspnetUser identityUser = await this.UserManager.GetUserAsync(principal);
            IList<string> roles = await this.UserManager.GetRolesAsync(identityUser);
            User domainUser = AuthenticationEntityConverter.ConvertToDomainUser(identityUser, roles);

            return domainUser;
        }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <remarks>Doesn't throw <see cref="System.ArgumentNullException"/>.</remarks>
        /// <param name="principal">The principal.</param>
        /// <returns>true if the user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated(ClaimsPrincipal principal)
        {
            return principal?.Identity.IsAuthenticated ?? false;
        }
    }
}
