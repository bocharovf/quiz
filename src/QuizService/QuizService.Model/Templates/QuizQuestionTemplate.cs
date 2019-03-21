namespace QuizService.Model
{
    /// <summary>
    /// Represents many to many relation between <see cref="QuizTemplate"/> and <see cref="QuestionTemplate"/>. 
    /// </summary>
    public class QuizQuestionTemplate
    {
        private QuizQuestionTemplate()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="QuizQuestionTemplate"/>.
        /// </summary>
        /// <param name="quizTemplate">Quiz template.</param>
        /// <param name="questionTemplate">Question template.</param>
        /// <param name="order">Question template order.</param>
        public QuizQuestionTemplate(QuizTemplate quizTemplate, QuestionTemplate questionTemplate, int order)
        {
            this.QuizTemplate = quizTemplate;
            this.QuestionTemplate = questionTemplate;
            this.Order = order;
        }

        /// <summary>
        /// Gets or sets value which determine whether question template is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the order in which question templates are displayed.
        /// </summary>
        /// <remarks>Question templates are sorted by ascending.</remarks>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets quiz template.
        /// </summary>
        public QuizTemplate QuizTemplate { get; set; }
        public int QuizTemplateId { get; set; }

        /// <summary>
        /// Gets or sets question template.
        /// </summary>
        public QuestionTemplate QuestionTemplate { get; set; }
        public int QuestionTemplateId { get; set; }
    }
}
