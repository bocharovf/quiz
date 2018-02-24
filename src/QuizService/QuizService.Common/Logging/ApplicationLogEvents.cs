namespace QuizService.Common.Logging
{
    /// <summary>
    /// Provides application specific event codes.
    /// </summary>
    /// <remarks>
    /// Reserves range from 10000 to 19999.
    /// </remarks>
    public static class ApplicationLogEvents
    {
        /// <summary>
        /// Occurs when new application request are received.
        /// </summary>
        public const int IncomingRequest = 10000;

        /// <summary>
        /// Occurs when application register client exception.
        /// </summary>
        public const int ClientError = 19998;

        /// <summary>
        /// Occurs when the request is failed with error.
        /// </summary>
        public const int RequestError = 19999;
    }
}
