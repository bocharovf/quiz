using Microsoft.AspNetCore.Identity;

namespace QuizService.DataAccess.Auth
{
    /// <summary>
    /// Represents application user.
    /// </summary>
    /// <remarks>Used in ASP.NET Core Identity infrastructure.</remarks>
    public class AspnetUser : IdentityUser<int>
    {
        public AspnetUser()
        {

        }

        public AspnetUser(string userName): base(userName)
        {

        }
    }
}
