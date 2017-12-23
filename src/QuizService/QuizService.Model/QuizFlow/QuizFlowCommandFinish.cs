namespace QuizService.DataContract.QuizFlow
{
    public class QuizFlowCommandFinish : QuizFlowCommand
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizFinish;
    }
}
