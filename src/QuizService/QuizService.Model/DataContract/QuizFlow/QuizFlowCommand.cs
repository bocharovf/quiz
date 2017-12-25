namespace QuizService.Model.DataContract
{
    public abstract class QuizFlowCommand
    {
        public virtual QuizFlowCommandType CommandType { get; }
    }
}
