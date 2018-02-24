namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents client application exception.
    /// </summary>
    public class ClientExceptionContract
    {
        /// <summary>
        /// Gets or sets code of error type.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets displayed error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets correlation identifier.
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets client exception stack trace.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets client agent browser and OS version.
        /// </summary>
        public string ClientPlatform { get; set; }
        
    }
}
