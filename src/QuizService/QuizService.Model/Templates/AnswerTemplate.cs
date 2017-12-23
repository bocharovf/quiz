using System;
using System.Collections.Generic;
using System.Text;

namespace QuizService.Model
{
    public class AnswerTemplate
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int Order { get; set; }
    }
}
