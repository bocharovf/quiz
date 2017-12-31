using System;
using System.Collections.Generic;
using System.Text;

namespace QuizService.Model
{
    public class Answer
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }

        public bool IsCorrect { get; set; }
    }
}
