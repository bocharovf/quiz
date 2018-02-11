namespace QuizService.Model.DataContract
{
    /// <summary>
    /// Contract for quiz template details page.
    /// </summary>
    public class QuizTemplateDetailsContract
    {
        public QuizTemplateDetailsContract(QuizTemplate quizTemplate, int questionsCount)
        {
            this.QuizTemplate = quizTemplate;
            this.QuestionsCount = questionsCount;
        }

        /// <summary>
        /// Gets or sets quiz template.
        /// </summary>
        public QuizTemplate QuizTemplate { get; set; }

        /// <summary>
        /// Gets or sets amount of questions in quiz template.
        /// </summary>
        public int QuestionsCount { get; set; }
    }
}
