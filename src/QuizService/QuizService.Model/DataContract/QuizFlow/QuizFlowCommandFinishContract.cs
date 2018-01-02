namespace QuizService.Model.DataContract
{
    public class QuizFlowCommandFinishContract : QuizFlowCommandContract
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizFinish;

        public override void HideAnswerCorrectness()
        {
            // Left empty on purpose.
        }
    }
}
