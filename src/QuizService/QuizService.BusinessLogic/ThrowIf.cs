using QuizService.BusinessLogic.Exceptions;
using QuizService.Model;
using System;

namespace QuizService.BusinessLogic
{
    public static class ThrowIf
    {
        public static void NotFound<T, K>(T entity,K key)
        {
            if (entity == null)
                throw new EntityNotFoundException(typeof(T), key);
        }

        public static void Null(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void Completed(Quiz quiz)
        {
            if (quiz != null && quiz.IsCompleted)
            {
                throw new QuizFlowException(
                    QuizFlowErrorCodes.QuizAlreadyCompleted,
                    "Quiz already completed."
                );
            }
        }
    }
}
