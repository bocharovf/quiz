using QuizService.Model.Exceptions;

namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Defines service exception.
    /// </summary>
    public class ServiceExceptionContract : IServiceExceptionContract
    {
        private IBusinessLogicException Exception;

        /// <summary>
        /// Gets code of error type.
        /// </summary>
        public string ErrorCode => this.Exception.ErrorCode;

        /// <summary>
        /// Gets displayed error message.
        /// </summary>
        public string Message => this.Exception.Message;

        /// <summary>
        /// Gets additional exception properties.
        /// </summary>
        public object Extension => this.Exception.Extension;

        public ServiceExceptionContract(IBusinessLogicException businessLogicException)
        {
            this.Exception = businessLogicException;
        }
    }
}
