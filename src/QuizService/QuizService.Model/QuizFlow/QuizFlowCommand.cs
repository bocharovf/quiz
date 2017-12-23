namespace QuizService.DataContract.QuizFlow
{
    public abstract class QuizFlowCommand
    {
        public virtual QuizFlowCommandType CommandType { get; }
    }
}
