namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents authentication status information.
    /// </summary>
    public class AuthenticationStatusContract
    {
        /// <summary>
        /// Gets or sets value indication whether use is signed in.
        /// </summary>
        public bool IsSignedIn { get; set; }

        /// <summary>
        /// Gets or sets current user.
        /// </summary>
        public User User { get; set; }
    }
}
