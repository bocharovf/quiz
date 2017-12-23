using QuizService.Model;

namespace QuizService.DataContract.QuizFlow
{
    public class QuizFlowCommandProceed : QuizFlowCommand
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizProceed;

        public Question Question { get; }

        public QuizFlowCommandProceed(Question question)
        {
            this.Question = question;
        }
    }
}
