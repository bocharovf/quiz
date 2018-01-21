using QuizService.Model.Exceptions;
using System;

namespace QuizService.BusinessLogic.Exceptions
{
    [Serializable]
    public class QuizFlowException : Exception, IBusinessLogicException
    {
        private string CustomErrorCode;

        public string ErrorCode => this.CustomErrorCode;

        public object Extension => null;

        public QuizFlowException(QuizFlowErrorCodes errorCode, string message) : this(errorCode, message, null) { }

        public QuizFlowException(QuizFlowErrorCodes errorCode, string message, Exception inner): base(message, inner)
        {
            this.CustomErrorCode = errorCode.ToString();
        }
    }
}
