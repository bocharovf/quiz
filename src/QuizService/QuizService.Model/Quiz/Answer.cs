namespace QuizService.Model
{
    /// <summary>
    /// Represents answer for quiz question.
    /// </summary>
    public class Answer
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets answer template identifier.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets value which determine whether answer is correct.
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}
