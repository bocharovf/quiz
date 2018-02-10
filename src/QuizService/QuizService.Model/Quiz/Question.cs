using System;
using System.Collections.Generic;

namespace QuizService.Model
{
    /// <summary>
    /// Represents quiz question.
    /// </summary>
    public class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
        }

        public int Id { get; set; }

        /// <summary>
        /// Gets or sets quiz to which the question belongs.
        /// </summary>
        public Quiz Quiz { get; set; }

        /// <summary>
        /// Gets or sets question template identifier.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets date and time when question started.
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Gets or sets date and time when question finished.
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Gets or sets collection of answers for the question.
        /// </summary>
        public ICollection<Answer> Answers { get; set; }

        /// <summary>
        /// Gets or sets sequential number of the question inside quiz.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets value which determine whether question is answered.
        /// </summary>
        public bool IsAnswered => DateEnd.HasValue;
    }
}
