using System.Collections.Generic;

namespace QuizService.Model
{
    public class QuizTemplate
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public ICollection<QuizQuestionTemplate> QuizQuestions { get; set; } = new List<QuizQuestionTemplate>();
    }
}
