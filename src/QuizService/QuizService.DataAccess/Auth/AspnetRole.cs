using Microsoft.AspNetCore.Identity;

namespace QuizService.DataAccess.Auth
{
    /// <summary>
    /// Represents application role.
    /// </summary>
    /// <remarks>Used in ASP.NET Core Identity infrastructure.</remarks>
    public class AspnetRole: IdentityRole<int>
    {
        public AspnetRole()
        {

        }

        public AspnetRole(string roleName) : base(roleName)
        {

        }
    }
}
