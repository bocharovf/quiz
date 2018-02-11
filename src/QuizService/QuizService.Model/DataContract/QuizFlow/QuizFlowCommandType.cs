namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Command type to manage quiz flow.
    /// </summary>
    public enum QuizFlowCommandType
    {
        Unknown,

        /// <summary>
        /// Command to proceed the qiuz with the next question.
        /// </summary>
        QuizProceed,

        /// <summary>
        /// Command to finish quiz.
        /// </summary>
        QuizFinish
    }
}
