namespace QuizService.BusinessLogic.Exceptions
{
    public enum QuizFlowErrorCodes
    {
        Unknown,
        LastQuestionNotAnswered,
        QuestionAlreadyAnswered,
        QuizAlreadyCompleted
    }
}
