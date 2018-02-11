using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model
{
    /// <summary>
    /// Represents template for quiz question.
    /// </summary>
    public class QuestionTemplate
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets displayed question text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets question type.
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Gets or sets collection of answer templates.
        /// </summary>
        public ICollection<AnswerTemplate> Answers { get; set; } = new List<AnswerTemplate>();

        /// <summary>
        /// Gets answer template by identifier.
        /// </summary>
        /// <param name="templateId">Answer template identifier.</param>
        /// <returns>Returns answer template if it is found; returns null otherwise.</returns>
        public AnswerTemplate GetAnswer(int templateId)
        {
            return this.Answers.FirstOrDefault(q => q.Id == templateId);
        }
    }
}
