using QuizService.Interfaces.Repository;
using System;

namespace QuizService.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IQuizRepository QuizRepository { get; }
        IQuizTemplateRepository QuizTemplateRepository { get; }
        IQuestionTemplateRepository QuestionTemplateRepository { get; }
        IScoreRepository ScoreRepository { get; }

        void Save();
    }
}