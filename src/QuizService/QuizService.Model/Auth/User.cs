using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model
{
    /// <summary>
    /// Represents application user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Represents anonymous user.
        /// </summary>
        public static readonly User Anonymous = new User() {
            Id = 0,
            Name = "Guest",
            Email = string.Empty
        };

        public User()
        {
            this.Roles = new List<string>();
        }

        /// <summary>
        /// Gets or sets unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets display name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or sets email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user roles collection.
        /// </summary>
        public ICollection<string> Roles { get; set; }

        /// <summary>
        /// Checks whether the user has specific role.
        /// </summary>
        /// <param name="role">Role to check.</param>
        /// <returns>True if user has specified role; False otherwise.</returns>
        public bool HasRole(string role) => this.Roles.Any(r => r == role);

        /// <summary>
        /// Gets a value indicating whether the user is administrator.
        /// </summary>
        public bool IsAdmin => this.HasRole(ApplicationRole.Admin);
    }
}
