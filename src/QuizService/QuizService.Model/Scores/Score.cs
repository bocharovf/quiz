﻿namespace QuizService.Model
{
    /// <summary>
    /// Represents quiz scores.
    /// </summary>
    public class Score
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets quiz identifier for which scores are calculated.
        /// </summary>
        public int QuizId { get; set; }

        /// <summary>
        /// Gets or sets scores amount.
        /// </summary>
        public decimal ScoresAmount { get; set; }
    }
}
