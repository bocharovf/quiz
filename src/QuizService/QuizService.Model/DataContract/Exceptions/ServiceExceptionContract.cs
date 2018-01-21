using QuizService.Model.Exceptions;

namespace QuizService.Model.DataContract
{
    public class ServiceExceptionContract : IServiceExceptionContract
    {
        private IBusinessLogicException Exception;

        public string ErrorCode => this.Exception.ErrorCode;

        public string Message => this.Exception.Message;

        public object Extension => this.Exception.Extension;

        public ServiceExceptionContract(IBusinessLogicException businessLogicException)
        {
            this.Exception = businessLogicException;
        }
    }
}
