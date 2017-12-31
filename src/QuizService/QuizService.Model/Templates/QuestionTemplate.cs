using System.Collections.Generic;
using System.Linq;

namespace QuizService.Model
{
    public class QuestionTemplate
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public ICollection<AnswerTemplate> Answers { get; set; } = new List<AnswerTemplate>();

        public AnswerTemplate GetAnswer(int templateId)
        {
            return this.Answers.FirstOrDefault(q => q.Id == templateId);
        }
    }
}
