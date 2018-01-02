namespace QuizService.Model.DataContract
{
    public class QuizTemplateDetailsContract
    {
        public QuizTemplateDetailsContract(QuizTemplate quizTemplate, int questionsCount)
        {
            this.QuizTemplate = quizTemplate;
            this.QuestionsCount = questionsCount;
        }

        public QuizTemplate QuizTemplate { get; set; }

        public int QuestionsCount { get; set; }
    }
}
