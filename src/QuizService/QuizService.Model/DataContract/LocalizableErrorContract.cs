namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents localizable error contract.
    /// </summary>
    public class LocalizableErrorContract
    {
        public LocalizableErrorContract()
        {

        }

        public LocalizableErrorContract(string code) : this(code, null)
        {

        }

        public LocalizableErrorContract(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// Unique error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Default error message.
        /// </summary>
        public string Message { get; set; }
    }
}
