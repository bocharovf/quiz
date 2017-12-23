namespace QuizService.Model
{
    public class QuizQuestionTemplate
    {
        public QuizQuestionTemplate()
        {

        }

        public QuizQuestionTemplate(QuizTemplate quizTemplate, QuestionTemplate questionTemplate, int order)
        {
            this.QuizTemplate = quizTemplate;
            this.QuestionTemplate = questionTemplate;
            this.Order = order;
        }

        public bool Enabled { get; set; }

        public int Order { get; set; }

        public QuizTemplate QuizTemplate { get; set; }
        public int QuizTemplateId { get; set; }

        public QuestionTemplate QuestionTemplate { get; set; }
        public int QuestionTemplateId { get; set; }
    }
}
