namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Represents base class for quiz flow commands.
    /// </summary>
    public abstract class QuizFlowCommandContract
    {
        /// <summary>
        /// Gets command type.
        /// </summary>
        public virtual QuizFlowCommandType CommandType { get; }

        /// <summary>
        /// Removes information of answer correctness to prevent cheating.
        /// </summary>
        public abstract void HideAnswerCorrectness();
    }
}
