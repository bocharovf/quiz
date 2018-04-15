namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents user login information.
    /// </summary>
    public class LoginContract
    {
        /// <summary>
        /// Gets or sets login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether user should stay signed 
        /// after closing browser.
        /// </summary>
        public bool Remember { get; set; }
    }
}
