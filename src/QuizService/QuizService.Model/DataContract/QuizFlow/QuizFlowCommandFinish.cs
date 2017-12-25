namespace QuizService.Model.DataContract
{
    public class QuizFlowCommandFinish : QuizFlowCommand
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizFinish;
    }
}
