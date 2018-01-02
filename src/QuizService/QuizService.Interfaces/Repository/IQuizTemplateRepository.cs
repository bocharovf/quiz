using QuizService.Interfaces.Common;
using QuizService.Model;
using System;
using System.Collections.Generic;

namespace QuizService.Interfaces.Repository
{
    public interface IQuizTemplateRepository: IGenericRepository<QuizTemplate, int>
    {
        int GetQuestionTemplateCount(int quizTemplateId);
    }
}
