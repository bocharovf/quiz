namespace QuizService.Model.Exceptions
{
    public class ServiceExceptionContract
    {
        private BusinessLogicException Exception;

        public string ErrorCode => this.Exception.ErrorCode;

        public string Message => this.Exception.Message;

        public object Extension => this.Exception.Extension;

        public ServiceExceptionContract(BusinessLogicException businessLogicException)
        {
            this.Exception = businessLogicException;
        }
    }
}
