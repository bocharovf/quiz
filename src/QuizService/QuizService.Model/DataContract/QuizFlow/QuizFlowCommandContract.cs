namespace QuizService.Model.DataContract
{
    public abstract class QuizFlowCommandContract
    {
        public virtual QuizFlowCommandType CommandType { get; }

        public abstract void HideAnswerCorrectness();
    }
}
