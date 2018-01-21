using QuizService.Model.Exceptions;
using System;

namespace QuizService.BusinessLogic.Exceptions
{
    [Serializable]
    public class BusinessLogicException : Exception, IBusinessLogicException
    {
        public virtual string ErrorCode { get; }

        public virtual object Extension { get; }

        public BusinessLogicException() { }

        public BusinessLogicException(string errorCode) : this(errorCode, null) {}

        public BusinessLogicException(string errorCode, string message) : this(errorCode, message, null) { }

        public BusinessLogicException(string errorCode, string message, Exception inner) : base(message, inner) {
            this.ErrorCode = errorCode;
        }

        protected BusinessLogicException(
          string errorCode,
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) {
            this.ErrorCode = errorCode;
        }
    }
}
