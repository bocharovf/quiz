using QuizService.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model
{
    /// <summary>
    /// Represents quiz.
    /// </summary>
    public class Quiz : IUserOwnedResource
    {
        public Quiz()
        {
            this.Questions = new List<Question>();
        }

        public int Id { get; set; }

        /// <summary>
        /// Gets or sets quiz template identifier.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or sets date and time when quiz started.
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Gets or sets date and time when quiz finished.
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Gets or sets collection of quiz questions.
        /// </summary>
        public ICollection<Question> Questions { get; set; }

        /// <summary>
        /// Gets or sets identifier of user who created the quiz.
        /// </summary>
        public int CreatedUserId { get; set; }

        /// <summary>
        /// Gets the value which determine whether quiz completed.
        /// </summary>
        public bool IsCompleted => DateEnd.HasValue;

        /// <summary>
        /// Gets current question of the quiz.
        /// </summary>
        public Question CurrentQuestion
        {
            get
            {
                return Questions.OrderByDescending(q => q.Order).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets quiz question by question identifier.
        /// </summary>
        /// <param name="questionId">Question identifier.</param>
        /// <returns>Returns question if it is found; returns null otherwise.</returns>
        public Question GetQuestion(int questionId)
        {
            return this.Questions.FirstOrDefault(q => q.Id == questionId);
        }
    }
}
