namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Quiz flow command to finish quiz.
    /// </summary>
    public class QuizFlowCommandFinishContract : QuizFlowCommandContract
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizFinish;

        public override void HideAnswerCorrectness()
        {
            // Left empty on purpose.
        }
    }
}
