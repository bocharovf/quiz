using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuizService.DataAccess.Auth;
using QuizService.Interfaces.Services;
using QuizService.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizService.Auth
{
    /// <summary>
    /// Provides access to the current domain user.
    /// </summary>
    public class UserAccessorService : IUserAccessorService
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly UserManager<AspnetUser> UserManager;
        private readonly SignInManager<AspnetUser> SignInManager;

        /// <summary>
        /// Gets current domain user.
        /// </summary>
        public User DomainUser { get; }

        public UserAccessorService(
            UserManager<AspnetUser> userManager,
            SignInManager<AspnetUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.HttpContextAccessor = httpContextAccessor;

            this.DomainUser = this.GetDomainUserAsync().Result;
        }

        /// <summary>
        /// Gets a value that indicates whether the current user has been authenticated.
        /// </summary>
        /// <returns>true if the current user was authenticated; otherwise, false.</returns>
        public bool IsAuthenticated
        {
            get
            {
                ClaimsPrincipal principal = this.HttpContextAccessor.HttpContext.User;
                return principal?.Identity.IsAuthenticated ?? false;
            }
        }

        private async Task<User> GetDomainUserAsync()
        {
            if (!this.IsAuthenticated)
            {
                return User.Anonymous;
            }

            ClaimsPrincipal principal = this.HttpContextAccessor.HttpContext.User;
            AspnetUser identityUser = await this.UserManager.GetUserAsync(principal);
            IList<string> roles = await this.UserManager.GetRolesAsync(identityUser);
            User domainUser = AuthenticationEntityConverter.ConvertToDomainUser(identityUser, roles);

            return domainUser;
        }
    }
}
