using QuizService.Model;
using System;

namespace QuizService.BusinessLogic.Exceptions
{
    /// <summary>
    /// Defines an exception in quiz flow logic.
    /// </summary>
    [Serializable]
    public class QuizFlowException : BusinessLogicException
    {
        public QuizFlowException(QuizFlowErrorCodes errorCode, string message) : 
            this(errorCode, message, null) {
        }

        public QuizFlowException(QuizFlowErrorCodes errorCode, string message, Exception inner): 
            base(ConvertErrorCode(errorCode), message, inner) {
        }

        private static string ConvertErrorCode(QuizFlowErrorCodes errorCode) {
            return errorCode.ToString();
        }
    }
}
