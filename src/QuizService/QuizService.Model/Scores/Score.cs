using System;
using System.Collections.Generic;
using System.Text;

namespace QuizService.Model
{
    public class Score
    {
        public int Id { get; set; }

        public Quiz Quiz { get; set; }

        public decimal ScoresAmount { get; set; }
    }
}
