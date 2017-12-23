using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuizService.Model
{
    public class Quiz
    {
        public Quiz()
        {
            this.Questions = new List<Question>();
        }

        public int Id { get; set; }

        public QuizTemplate Template { get; set; }
        public int TemplateId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public List<Question> Questions { get; set; }

        public bool IsFinished => DateEnd.HasValue;

        public Question LastQuestion
        {
            get
            {
                return Questions.OrderByDescending(q => q.Order).FirstOrDefault();
            }
        }
        
    }
}
