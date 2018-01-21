using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model.DataContract
{
    public class QuizFlowCommandProceedContract : QuizFlowCommandContract
    {
        public override QuizFlowCommandType CommandType => QuizFlowCommandType.QuizProceed;
        public int QuizId { get; }
        public QuestionTemplate Template { get; }

        private Question question;
        public int QuestionId => question.Id;

        public QuizFlowCommandProceedContract(Question question, QuestionTemplate questionTemplate)
        {
            this.QuizId = question.Quiz.Id;
            this.question = question;
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
