namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Contains signin result codes.
    /// </summary>
    public enum SignInErrorCode
    {
        /// <summary>
        /// Unknown error.
        /// </summary>
        Unknown,

        /// <summary>
        /// User is locked out.
        /// </summary>
        IsLockedOut,

        /// <summary>
        /// Sign in is not allowed.
        /// </summary>
        IsNotAllowed,

        /// <summary>
        /// Sign in requires two factor authentication.
        /// </summary>
        RequiresTwoFactor,

        /// <summary>
        /// Invalid credentials specified.
        /// </summary>
        InvalidCredentials
    }
}
