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

        public int TemplateId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public ICollection<Question> Questions { get; set; }

        public bool IsCompleted => DateEnd.HasValue;

        public Question CurrentQuestion
        {
            get
            {
                return Questions.OrderByDescending(q => q.Order).FirstOrDefault();
            }
        }

        public Question GetQuestion(int questionId)
        {
            return this.Questions.FirstOrDefault(q => q.Id == questionId);
        }
    }
}
