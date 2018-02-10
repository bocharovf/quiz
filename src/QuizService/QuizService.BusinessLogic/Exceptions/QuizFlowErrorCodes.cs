namespace QuizService.BusinessLogic.Exceptions
{
    /// <summary>
    /// Quiz flow error code.
    /// </summary>
    public enum QuizFlowErrorCodes
    {
        Unknown,

        /// <summary>
        /// Question already answered.
        /// </summary>
        QuestionAlreadyAnswered,

        /// <summary>
        /// Quiz already completed.
        /// </summary>
        QuizAlreadyCompleted
    }
}
