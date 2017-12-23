using System;
using System.Collections.Generic;

namespace QuizService.Model
{
    public class Question
    {
        public Question()
        {
            this.Answers = new List<Answer>();
        }

        public int Id { get; set; }

        //public Quiz Quiz { get; set; }

        public QuestionTemplate Template { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public List<Answer> Answers { get; set; }

        public int Order { get; set; }

        public bool IsAnswered => DateEnd.HasValue;
    }
}
