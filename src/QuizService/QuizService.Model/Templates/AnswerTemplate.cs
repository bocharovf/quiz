namespace QuizService.Model
{
    /// <summary>
    /// Represents answer template.
    /// </summary>
    public class AnswerTemplate
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets displayed text of the answer.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets value which determine whether the answer is correct.
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Gets or sets answer display order.
        /// </summary>
        /// <remarks>Answers are sorted by ascending.</remarks>
        public int Order { get; set; }
    }
}
