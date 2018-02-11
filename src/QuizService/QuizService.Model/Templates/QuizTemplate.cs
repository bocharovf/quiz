namespace QuizService.Model
{
    /// <summary>
    /// Represents quiz template.
    /// </summary>
    public class QuizTemplate
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets displayed quiz template title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets quiz template description.
        /// </summary>
        public string Description { get; set; }
    }
}
