using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model.DataContract
{
    public class QuizFlowCommandProceedContract : QuizFlowCommandContract
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizProceed;
        public int QuizId { get; }
        public int QuestionId { get; }
        public QuestionTemplate Template { get; }

        public QuizFlowCommandProceedContract(Question question, QuestionTemplate questionTemplate)
        {
            this.QuizId = question.Quiz.Id;
            this.QuestionId = question.Id;
            this.Template = questionTemplate;
        }

        public override void HideAnswerCorrectness()
        {
            foreach (var answerTemplate in this.Template.Answers)
            {
                answerTemplate.IsCorrect = false;
            }
        }
    }
}
