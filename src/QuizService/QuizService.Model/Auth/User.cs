using System.Collections.Generic;

namespace QuizService.Model
{
    /// <summary>
    /// Represents application user.
    /// </summary>
    public class User
    {
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
    }
}
