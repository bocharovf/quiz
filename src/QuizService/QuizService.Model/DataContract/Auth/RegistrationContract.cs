namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents user registration information.
    /// </summary>
    public class RegistrationContract
    {
        /// <summary>
        /// Gets or sets user display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets account password.
        /// </summary>
        public string Password { get; set; }
    }
}
