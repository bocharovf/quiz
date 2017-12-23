using System.Collections.Generic;

namespace QuizService.Model
{
    public class QuestionTemplate
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        // public ICollection<QuizQuestionTemplate> QuizQuestions { get; set; } = new List<QuizQuestionTemplate>();

        public ICollection<AnswerTemplate> Answers { get; set; } = new List<AnswerTemplate>();
    }
}
