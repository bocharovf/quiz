using QuizService.Model;
using System.Collections.Generic;

namespace QuizService.DataAccess.Auth
{
    /// <summary>
    /// Provides methods to convert Asp.net identity users to domain users.
    /// </summary>
    public class AuthenticationEntityConverter
    {
        /// <summary>
        /// Converts identity user <see cref="AspnetUser"/> to domain <see cref="User"/>.
        /// </summary>
        /// <param name="user">User to convert.</param>
        /// <param name="roles">User roles.</param>
        /// <returns>Domain user.</returns>
        public static User ConvertToDomainUser(AspnetUser user, IList<string> roles) {
            var domainUser = new User()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Roles = roles
            };

            return domainUser;
        }
    }
}
